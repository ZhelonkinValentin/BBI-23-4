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

   
    public Competition(string lastName, double[] styleScores, double jumpDistance)
     {
         this.lastName = lastName;
         this.styleScores = styleScores;
         this.jumpDistance = jumpDistance;
         this.totalScore = 0;
       CalculateTotalScore();
     }

   
     protected abstract void CalculateTotalScore();


     public string GetLastName()
     {
         return this.lastName;
     }

     public double GetTotalScore()
     {
         return this.totalScore;
     }
 }


 public class SkiJumpCompetition : Competition
 {
     public SkiJumpCompetition(string lastName, double[] styleScores, double jumpDistance)
         : base(lastName, styleScores, jumpDistance)
     {
     }

     protected override void CalculateTotalScore()
     {
         const int    NumJudges = 5;
         const double BaseDistance = 120.0;
         const double BaseScore = 60.0;
         const double ScorePerMeter = 2.0; 
         const double PenaltyPerMeter = -2.0; 

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
 }
    public class LongJumpCompetition : Competition
   {
     public LongJumpCompetition(string lastName, double[] styleScores, double jumpDistance)
         : base(lastName, styleScores, jumpDistance)
     {
     }

     protected override void CalculateTotalScore()
     {
         const int    NumJudges = 5;
         const double BaseDistance = 120.0;
         const double BaseScore = 60.0;
         const double ScorePerMeter = 2.0; 
         const double PenaltyPerMeter = -2.0; 

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
 }

 class Program
 {
     static void Main(string[] args)
     {
         Competition[] competitions = new Competition[5];
         competitions[0] = new SkiJumpCompetition("Трамп", new double[] { 18.5, 17.5, 19.0, 20.0, 18.0 }, 122.0);
         competitions[1] = new LongJumpCompetition("Обама", new double[] { 19.0, 18.0, 17.0, 16.5, 18.5 }, 118.0);
         competitions[2] = new SkiJumpCompetition("Путин", new double[] { 20.0, 19.5, 19.0, 18.5, 20.0 }, 125.0);
         competitions[3] = new LongJumpCompetition("Дзюба", new double[] { 17.5, 17.0, 18.5, 19.0, 18.0 }, 121.5);
         competitions[4] = new SkiJumpCompetition("Глушаков", new double[] { 19.5, 20.0, 19.0, 18.5, 19.5 }, 123.5);

         for (int i = 0; i < competitions.Length - 1; i++)
         {
             for (int j = i + 1; j < competitions.Length; j++)
             {
                 if (competitions[j].GetTotalScore() > competitions[i].GetTotalScore())               {
                     var temp = competitions[i];
                     competitions[i] = competitions[j];
                     competitions[j] = temp;
                 }
             }
         }

         
        Console.WriteLine("Соревнования по прыжкам в длину");
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

namespace _7и3
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
            PrintResults(FindWinner(groupWoman, groupMan));
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
        static Team[] FindWinner(Team[] group1, Team[] group2)  
        {
            if (TotalScore(group1) > TotalScore(group2)) { return group1; }
            else { return group2; };
        }
        static void Sortirovka(Team[] info)
        {
            for (int i = 0; i < info.Length - 1; i++)  
            {
                for (int j = i + 1; j < info.Length; j++)
                {
                    if (info[j].Result > info[i].Result)
                    {
                        Team pomoch = info[j];
                        info[j] = info[i];
                        info[i] = pomoch;
                    }
                }
            }
        }
    }
}



