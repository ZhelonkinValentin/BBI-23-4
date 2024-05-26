using System;
using System.IO;
using Newtonsoft.Json;
using ProtoBuf;
using System.Xml.Serialization;

namespace _7и3
{
    [ProtoContract]
    public abstract class Team
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public int Score { get; set; }

        public int Result => Score;

        public Team(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public abstract void Print();
    }

    [ProtoContract]
    public class WomanTeam : Team
    {
        public WomanTeam(string name, int score) : base(name, score) { }

        public override void Print()
        {
            Console.WriteLine($"{Name} (Woman): {Score}");
        }
    }

    [ProtoContract]
    public class ManTeam : Team
    {
        public ManTeam(string name, int score) : base(name, score) { }

        public override void Print()
        {
            Console.WriteLine($"{Name} (Man): {Score}");
        }
    }

    public abstract class Serializer<T>
    {
        protected string FilePath { get; set; }

        public Serializer(string filePath)
        {
            FilePath = filePath;
        }

        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }

    public class JsonSerializer<T> : Serializer<T>
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

    public class XmlSerializer<T> : Serializer<T>
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

    public class BinarySerializer<T> : Serializer<T>
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

    static class Program
    {
        static void Main(string[] args)
        {
            var womanTeamsPath = @"C:\path\to\womanTeams.json";
            var manTeamsPath = @"C:\path\to\manTeams.xml";
            var binaryPath = @"C:\path\to\binaryData.bin";

            var womanTeamsJsonSerializer = new JsonSerializer<WomanTeam>(womanTeamsPath);
            var manTeamsXmlSerializer = new XmlSerializer<ManTeam>(manTeamsPath);
            var binarySerializer = new BinarySerializer<Team>(binaryPath);

            var womanTeams = new WomanTeam[]
            {
                new WomanTeam("Желонкин", 1),
                new WomanTeam("Подземельный", 2),
                new WomanTeam("Трамп", 3),
                new WomanTeam("Обама", 4),
                new WomanTeam("Пушкин", 5)
            };

            var manTeams = new ManTeam[]
            {
                new ManTeam("Путин", 9),
                new ManTeam("Дураков", 8),
                new ManTeam("Лисицин", 7),
                new ManTeam("Андреев", 6),
                new ManTeam("Жопкин", 5)
            };

            womanTeamsJsonSerializer.Serialize(womanTeams);
            manTeamsJsonSerializer.Serialize(manTeams);


            var loadedWomanTeams = womanTeamsJsonSerializer.Deserialize<WomanTeam[]>();
            var loadedManTeams = manTeamsJsonSerializer.Deserialize<ManTeam[]>();

            manTeamsXmlSerializer.Serialize(manTeams);

            var loadedManTeamsFromXml = manTeamsXmlSerializer.Deserialize<ManTeam[]>();

            binarySerializer.Serialize(new WomanTeam[] { womanTeams[0], womanTeams[1] });

            var loadedTeamsFromBinary = binarySerializer.Deserialize<WomanTeam[]>();

            foreach (var team in loadedWomanTeams.Concat(loadedManTeams).Concat(loadedManTeamsFromXml).Concat(loadedTeamsFromBinary))
            {
                team.Print();
            }
        }
    }
}