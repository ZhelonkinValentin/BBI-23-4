using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ProtoBuf;


abstract class Serializer<T>
{
    protected string FilePath { get; set; }

    public Serializer(string filePath)
    {
        FilePath = filePath;
    }

    public abstract void Serialize(T obj);
    public abstract T Deserialize();
}


class JsonSerializer<T> : Serializer<T>
{
    public JsonSerializer(string filePath) : base(filePath) { }

    public override void Serialize(T obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(FilePath, json);
    }

    public override T Deserialize()
    {
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}

class XmlSerializer<T> : Serializer<T>
{
    public XmlSerializer(string filePath) : base(filePath) { }

    public override void Serialize(T obj)
    {
        var settings = new XmlSerializerSettings { Indented = true };
        using var writer = XmlWriter.Create(FilePath, settings);
        var serializer = new XmlSerializer(typeof(T), settings);
        serializer.Serialize(writer, obj);
    }

    public override T Deserialize()
    {
        var settings = new XmlSerializerSettings();
        using var reader = XmlReader.Create(FilePath, settings);
        var serializer = new XmlSerializer(typeof(T), settings);
        return (T)serializer.Deserialize(reader);
    }
}

class BinarySerializer<T> : Serializer<T>
{
    public BinarySerializer(string filePath) : base(filePath) { }

    public override void Serialize(T obj)
    {
        var formatter = new BinaryFormatter();
        using var stream = new FileStream(FilePath, FileMode.Create);
        formatter.Serialize(stream, obj);
    }

    public override T Deserialize()
    {
        var formatter = new BinaryFormatter();
        using var stream = new FileStream(FilePath, FileMode.Open);
        return (T)formatter.Deserialize(stream);
    }
}

[ProtoContract]
public abstract class Competition
{
    [ProtoMember(1)]
    public string LastName { get; set; }

    [ProtoMember(2)]
    public double[] StyleScores { get; set; }

    [ProtoMember(3)]
    public double JumpDistance { get; set; }

    [ProtoMember(4)]
    public double TotalScore { get; private set; }

    protected Competition(string lastName, double[] styleScores, double jumpDistance)
    {
        LastName = lastName;
        StyleScores = styleScores;
        JumpDistance = jumpDistance;
        CalculateTotalScore();
    }

    protected virtual void CalculateTotalScore()
    {
        double sum = 0;
        for (int k = 1; k < StyleScores.Length - 1; k++)
        {
            sum += StyleScores[k];
        }
        double distanceDifference = JumpDistance - 120.0;
        double distanceScore = distanceDifference > 0
         ? 60.0 + distanceDifference * 2.0
             : 60.0 + distanceDifference * (-2.0);

        TotalScore = sum + distanceScore;
    }

    public string GetLastName()
    {
        return LastName;
    }

    public double GetTotalScore()
    {
        return TotalScore;
    }
}

public abstract class SkiJump : Competition
{
    public string DisciplineName { get; protected set; }
}

public class SkiJump120m : SkiJump
{
    public SkiJump120m(string lastName, double[] styleScores, double jumpDistance)
        : base(lastName, styleScores, jumpDistance)
    {
        DisciplineName = "120м";
    }
}

public class SkiJump180m : SkiJump
{
    public SkiJump180m(string lastName, double[] styleScores, double jumpDistance)
        : base(lastName, styleScores, jumpDistance)
    {
        DisciplineName = "180м";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var competitions = new Competition[]
        {
            new SkiJump120m("Трамп", new double[] { 18.5, 17.5, 19.0, 20.0, 18.0 }, 122.0),
            new SkiJump120m("Обама", new double[] { 19.0, 18.0, 17.0, 16.5, 18.5 }, 118.0),
            new SkiJump180m("Путин", new double[] { 20.0, 19.5, 19.0, 18.5, 20.0 }, 125.0),
            new SkiJump120m("Дзюба", new double[] { 17.5, 17.0, 18.5, 19.0, 18.0 }, 121.5),
            new SkiJump180m("Глушаков", new double[] { 19.5, 20.0, 19.0, 18.5, 19.5 }, 123.5)
        };

        Array.Sort(competitions, (a, b) => b.GetTotalScore().CompareTo(a.GetTotalScore()));

        Console.WriteLine($"Соревнования по прыжкам на лыжах ({competitions[0].DisciplineName})");
        Console.WriteLine("Результаты:");
        for (int i = 0; i < competitions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {competitions[i].GetLastName()}: {competitions[i].GetTotalScore()} очков");
        }

        var jsonSerializer = new JsonSerializer<Competition>("competitions.json");
        foreach (var competition in competitions)
        {
            jsonSerializer.Serialize(competition);
        }

        var deserializedCompetitionsJson = jsonSerializer.Deserialize<Competition[]>();
        foreach (var competition in deserializedCompetitionsJson)
        {
            Console.WriteLine($"Deserialize Info JSON: {competition.GetLastName()}, Total Score: {competition.GetTotalScore()}");
        }

        var xmlSerializer = new XmlSerializer<Competition>("competitions.xml");
        foreach (var competition in competitions)
        {
            xmlSerializer.Serialize(competition);
        }

        var deserializedCompetitionsXml = xmlSerializer.Deserialize<Competition[]>();
        foreach (var competition in deserializedCompetitionsXml)
        {
            Console.WriteLine($"Deserialize Info XML: {competition.GetLastName()}, Total Score: {competition.GetTotalScore()}");
        }

        var binarySerializer = new BinarySerializer<Competition>("competitions.bin");
        foreach (var competition in competitions)
        {
            binarySerializer.Serialize(competition);
        }

        var deserializedCompetitionsBinary = binarySerializer.Deserialize<Competition[]>();
        foreach (var competition in deserializedCompetitionsBinary)
        {
            Console.WriteLine($"Deserialize Info Binary: {competition.GetLastName()}, Total Score: {competition.GetTotalScore()}");
        }
    }
}