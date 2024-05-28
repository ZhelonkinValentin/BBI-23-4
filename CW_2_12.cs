using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
     
        string inputString1 = "Привет, 9999"; 
        double frequency = CalculateMostFrequentLetterFrequency(inputString1);
        Console.WriteLine($"Частота самой часто встречающейся буквы: {frequency}");

    
        string inputString2 = "Sample input string with numbers 1, 2, and 30, -4";
        double average = CalculateAverageOfNumbersInString(inputString2);
        Console.WriteLine($"Среднее арифметическое чисел в строке: {average}");

     
        CreateDirectoryAndFiles();
    }

    static double CalculateMostFrequentLetterFrequency(string input)
    {
        var letters = input.ToLower().Where(char.IsLetter); 
        var letterFrequencies = letters.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count()); 
        if (letterFrequencies.Count == 0)
        {
            return 0; 
        }
        var mostFrequentLetter = letterFrequencies.Aggregate((l, r) => l.Value > r.Value ? l : r).Key; 
        return (double)letterFrequencies[mostFrequentLetter] / letters.Count(); 
    }

    static double CalculateAverageOfNumbersInString(string input)
    {
        var numbers = input.Split(new char[] { ' ', ',', '.', '-', ';' }, StringSplitOptions.RemoveEmptyEntries)
                           .Where(s => int.TryParse(s, out _))
                           .Select(int.Parse);
        if (!numbers.Any())
        {
            return 0; 
        }
        return numbers.Average();
    }

    static void CreateDirectoryAndFiles()
    {
        string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "m23...", "Answer");

        string filePath1 = Path.Combine(directoryPath, "cw2_1.json");
        string filePath2 = Path.Combine(directoryPath, "cw2_2.json");

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Директория не существует.");
            return;
        }

        if (!File.Exists(filePath1))
        {
            File.WriteAllText(filePath1, JsonConvert.SerializeObject(new { Data = "Some data for cw2_1" }));
        }

        if (!File.Exists(filePath2))
        {
            File.WriteAllText(filePath2, JsonConvert.SerializeObject(new { Data = "Some data for cw2_2" }));
        }
    }
}