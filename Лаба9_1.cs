using System;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;
using System.Reflection;

namespace _7
{
    internal class Program
    {
        [ProtoContract]
        public class Info
        {
            [ProtoMember(1)]
            public string Name { get; set; }
            [ProtoMember(2)]
            public string Society { get; set; }
            [ProtoMember(3)]
            public int FirstResult { get; set; }
            [ProtoMember(4)]
            public int SecondResult { get; set; }
            [ProtoMember(5)]
            public bool Disqualified { get; set; }

            public int Summ => FirstResult + SecondResult;

            public Info(string name, string society, int firstResult, int secondResult)
            {
                Name = name;
                Society = society;
                FirstResult = firstResult;
                SecondResult = secondResult;
                Disqualified = false;
            }

            public void Disqual() { Disqualified = true; }

            public void Print() { if (!Disqualified) { Console.WriteLine($"{Name}\t{Society}\t{Summ}"); } }
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
                var serializer = new XmlSerializer(typeof(T));
                using var writer = new StreamWriter(FilePath);
                serializer.Serialize(writer, obj);
            }

            public override T Deserialize()
            {
                var serializer = new XmlSerializer(typeof(T));
                using var reader = new StreamReader(FilePath);
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

        static void Main(string[] args)
        {
            var info = new[]
            {
                new Info("Марк", "ББИ-23-4", 200, 300),
                new Info("Валентин", "ББИ-23-3", 100, 200),
                new Info("Дмитрий", "ББИ-23-2", 155, 185),
                new Info("Юрий", "ББИ-23-1", 101, 204),
                new Info("Денис", "ББИ-23-4", 130, 190)
            };

            info[2].Disqual();

            Array.Sort(info, (a, b) => b.Summ.CompareTo(a.Summ));

            Console.WriteLine("Место\tИмя\tОбщество\tСумма результатов");
            for (int i = 0; i < info.Length; i++)
            {
                Console.Write($"{i + 1}\t");
                info[i].Print();
            }
            var jsonSerializer = new JsonSerializer<Info>("info.json");
            foreach (var item in info)
            {
                jsonSerializer.Serialize(item);
            }

            var deserializedInfoJson = jsonSerializer.Deserialize<Info[]>();
            foreach (var item in deserializedInfoJson)
            {
                Console.WriteLine($"Deserialize Info JSON: {item.Name}, Society: {item.Society}, Summ: {item.Summ}");
            }

            var xmlSerializer = new XmlSerializer<Info>("info.xml");
            foreach (var item in info)
            {
                xmlSerializer.Serialize(item);
            }

            var deserializedInfoXml = xmlSerializer.Deserialize<Info[]>();
            foreach (var item in deserializedInfoXml)
            {
                Console.WriteLine($"Deserialize Info XML: {item.Name}, Society: {item.Society}, Summ: {item.Summ}");
            }

            var binarySerializer = new BinarySerializer<Info>("info.bin");
            foreach (var item in info)
            {
                binarySerializer.Serialize(item);
            }

            var deserializedInfoBinary = binarySerializer.Deserialize<Info[]>();
            foreach (var item in deserializedInfoBinary)
            {
                Console.WriteLine($"Deserialize Info Binary: {item.Name}, Society: {item.Society}, Summ: {item.Summ}");
            }
        }
    }
}