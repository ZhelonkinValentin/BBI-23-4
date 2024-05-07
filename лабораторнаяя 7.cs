using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    internal class Program
    {
        class Info
        {
            protected string Name;
            protected string Society;
            protected int FirstResult;
            protected int SecondResult;
            protected bool Disqualified;
            public int Summ { get { return FirstResult + SecondResult; } }
            public Info(string name, string society, int firstResult, int secondResult)
            {
                Name = name;
                Society = society;
                FirstResult = firstResult;
                SecondResult = secondResult;
                Disqualified = false;
            }
            public void Disqual() { Disqualified = true; }
            public void Print() { if (Disqualified == false) { Console.WriteLine($"{Name}\t{Society}\t{Summ}"); } }
        }
        static void Main(string[] args)
        {
            Info[] info = new Info[5];

            info[0] = new Info("Марк", "ББИ-23-4", 200, 300);
            info[1] = new Info("Валентин", "ББИ-23-3", 100, 200);
            info[2] = new Info("Дмитрий", "ББИ-23-2", 155, 185);
            info[3] = new Info("Юрий", "ББИ-23-1", 101, 204);
            info[4] = new Info("Денис", "ББИ-23-4", 130, 190);

            info[2].Disqual();

            for (int i = 0; i < info.Length - 1; i++)
            {
                for (int j = i + 1; j < info.Length; j++)
                {
                    if (info[i].Summ < info[j].Summ)
                    {
                        Info pomoch = info[j];
                        info[j] = info[i];
                        info[i] = pomoch;
                    }
                }
            }

            Console.WriteLine("Место\tИмя\tОбщество\tСумма результатов");
            for (int i = 0; i < info.Length; i++)
            {
                Console.Write($"{i + 1}\t");
                info[i].Print();
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Competition
{
    protected string lastName;
    protected double[] styleScores;
    protected double jumpDistance;
    protected double totalScore;
    protected double BaseDistance { get; set; } = 120.0;
    protected double BaseScore { get; set; } = 60.0;
    protected double ScorePerMeter { get; set; } = 2.0;
    protected double PenaltyPerMeter { get; set; } = -2.0;
    public Competition(string lastName, double[] styleScores, double jumpDistance)
    {
        this.lastName = lastName;
        this.styleScores = styleScores;
        this.jumpDistance = jumpDistance;
        this.totalScore = 0;
        CalculateTotalScore();
    }

    protected void CalculateTotalScore()
    {
        double sum = 0;
        for (int k = 1; k < styleScores.Length - 1; k++)
        {
            sum += styleScores[k];
        }
        double distanceDifference = jumpDistance - BaseDistance;
        double distanceScore = distanceDifference > 0
           ? BaseScore + distanceDifference * ScorePerMeter
             : BaseScore + distanceDifference * PenaltyPerMeter;

        totalScore = sum + distanceScore;
    }

    public string GetLastName()
    {
        return this.lastName;
    }

    public double GetTotalScore()
    {
        return this.totalScore;
    }

    public static void MergeSort(Competition[] array)
    {
        if (array.Length <= 1) return;

        var mid = array.Length / 2;
        var left = new Competition[array.Length];
        var right = new Competition[array.Length];

        for (int i = 0; i < mid; i++) left[i] = array[i];
        for (int i = mid; i < array.Length; i++) right[i - mid] = array[i];

        MergeSort(left);
        MergeSort(right);

        Merge(array, left, right);
    }

    private static void Merge(Competition[] array, Competition[] left, Competition[] right)
    {
        int i = 0, j = 0, k = 0;
        while (i < left.Length && j < right.Length)
        {
            if (left[i].GetTotalScore() > right[j].GetTotalScore())
            {
                array[k++] = left[i++];
            }
            else
            {
                array[k++] = right[j++];
            }
        }

        while (i < left.Length) array[k++] = left[i++];
        while (j < right.Length) array[k++] = right[j++];
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
        Competition[] competitions = new Competition[5];
        competitions[0] = new SkiJump120m("Трамп", new double[] { 18.5, 17.5, 19.0, 20.0, 18.0 }, 122.0);
        competitions[1] = new SkiJump120m("Обама", new double[] { 19.0, 18.0, 17.0, 16.5, 18.5 }, 118.0);
        competitions[2] = new SkiJump180m("Путин", new double[] { 20.0, 19.5, 19.0, 18.5, 20.0 }, 125.0);
        competitions[3] = new SkiJump120m("Дзюба", new double[] { 17.5, 17.0, 18.5, 19.0, 18.0 }, 121.5);
        competitions[4] = new SkiJump180m("Глушаков", new double[] { 19.5, 20.0, 19.0, 18.5, 19.5 }, 123.5);

        Competition.MergeSort(competitions);

        Console.WriteLine($"Соревнования по прыжкам на лыжах ({competitions[0].DisciplineName})");
        Console.WriteLine("Результаты:");
        for (int i = 0; i < competitions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {competitions[i].GetLastName()}: {competitions[i].GetTotalScore()} очков");
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7i3
{
    internal class Program
    {
        abstract class Team
        {
            protected string Name;
            protected int Score;
            public int Result { get { return Score; } }
            public Team(string name, int score)
            {
                Name = name;
                Score = score;
            }
            public abstract void Print();
        }

        class WomanTeam : Team
        {
            public WomanTeam(string name, int score) : base(name, score) { }
            public override void Print()
            {
                Console.WriteLine($"{Name} (Woman): {Score}");
            }
        }

        class ManTeam : Team
        {
            public ManTeam(string name, int score) : base(name, score) { }
            public override void Print()
            {
                Console.WriteLine($"{Name} (Man): {Score}");
            }
        }

        static void Main(string[] args)
        {
            WomanTeam[] groupWoman = new WomanTeam[5] { new WomanTeam("Желонкин", 1), new WomanTeam("Подземельный", 2), new WomanTeam("Трамп", 3), new WomanTeam("Обама", 4), new WomanTeam("Пушкин", 5) };
            ManTeam[] groupMan = new ManTeam[5] { new ManTeam("Путин", 9), new ManTeam("Дураков", 8), new ManTeam("Лисицин", 7), new ManTeam("Андреев", 6), new ManTeam("Жопкин", 5) };

            Console.WriteLine("\nРезультаты женской команды:");
            Sortirovka(groupWoman);
            PrintResults(groupWoman);

            Console.WriteLine("\nРезультаты мужской команды:");
            Sortirovka(groupMan);
            PrintResults(groupMan);

            Console.WriteLine($"\nРезультаты команды победителей: ");
            Team winner = FindWinner(groupWoman, groupMan);
            winner.Print(); 
        }

        static void PrintResults(Team[] group)
        {
            foreach (var participant in group)
            {
                participant.Print();
            }
        }

        static int TotalScore(Team[] group)
        {
            int total = 0;
            foreach (var participant in group)
            {
                total += participant.Result;
            }
            return total;
        }

        static Team FindWinner(Team[] group1, Team[] group2)
        {
            
            Sortirovka(group1);
            Sortirovka(group2);

            
            if (TotalScore(group1) > TotalScore(group2))
            {
                return group1[0]; 
            }
            else
            {
                return group2[0]; 
            }
        }

        static void Sortirovka(Team[] info)
        {
            for (int i = 0; i < info.Length - 1; i++)
            {
                for (int j = i + 1; j < info.Length; j++)
                {
                    if (info[j].Result > info[i].Result)
                    {
                        Team temp = info[j];
                        info[j] = info[i];
                        info[i] = temp;
                    }
                }
            }
        }
    }
}