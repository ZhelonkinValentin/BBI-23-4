
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _8и1
{
    internal class Program
    {
        abstract class Task
        {
            protected string Text;
            public Task(string text)
            {
                Text = text;
            }
        }
        class Task1 : Task
        {
            private string Text1;
            private char[] Alphabet = new char[33] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
            private double[] Count = new double[33];
            private double[] Procent = new double[33];
            private double AllCount = 0;
            public Task1(string text) : base(text) { }
            public override string ToString()
            {
                Text1 = Text.ToLower();
                for (int i = 0; i < Text1.Length; i++)
                {
                    if (Char.IsDigit(Text1[i]) == false)
                    {
                        for (int j = 0; j < Alphabet.Length; j++)
                        {
                            if (Alphabet[j] == Text1[i])
                            {
                                Count[j]++;
                                AllCount++;
                            }
                        }
                    }
                }
                for (int i = 0; i < Alphabet.Length; i++)
                    Procent[i] = (Count[i] / AllCount) * 100;
                for (int i = 0; i < Alphabet.Length; i++)
                    Console.WriteLine($"{Alphabet[i]} - {Math.Round(Procent[i], 2)}%");
                return base.ToString();
            }
        }
        class Task3 : Task
        {
            private string Text1;
            private string[] Words;
            private int Max = 50;
            public Task3(string text) : base(text)
            {
                Text1 = Text;
                Words = Text1.Split(new Char[] { ' ' });
            }
            public override string ToString()
            {
                int k = 0;
                string Stroka = "";
                for (int i = 0; i < Words.Length; i++)
                {
                    k += Words[i].Length;
                    string s = Words[i];
                    if (k <= Max)
                    {
                        Stroka += Words[i] + " ";
                        k += 1;
                    }
                    else
                    {
                        Console.WriteLine(Stroka);
                        k = Words[i].Length + 1;
                        Stroka = Words[i] + " ";
                    }
                }
                Console.Write(Stroka);
                return base.ToString();
            }
        }
        class Task5 : Task
        {
            static void Sortirovka(char[] alphabet, double[] procent)
            {
                for (int i = 0; i < alphabet.Length - 1; i++)
                {
                    for (int j = i + 1; j < alphabet.Length; j++)
                    {
                        if (procent[j] > procent[i])
                        {
                            Char pomoch1 = alphabet[i];
                            double pomoch2 = procent[i];
                            alphabet[i] = alphabet[j];
                            procent[i] = procent[j];
                            alphabet[j] = pomoch1;
                            procent[j] = pomoch2;
                        }
                    }
                }
            }
            private string Text1;
            private string[] Words;
            private char[] Alphabet = new char[33] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
            private double[] Count = new double[33];
            private double[] Procent = new double[33];
            private double AllCount = 0;
            public Task5(string text) : base(text)
            {
                Text1 = Text.ToLower();
                Words = Text1.Split(new Char[] { ' ', '(', });
            }
            public override string ToString()
            {
                for (int i = 0; i < Text1.Length; i++)
                {
                    if (Char.IsDigit(Text1[i]) == false)
                    {
                        for (int j = 0; j < Alphabet.Length; j++)
                        {
                            if (Alphabet[j] == Text1[i])
                            {
                                Count[j]++;
                                AllCount++;
                            }
                        }
                    }
                }
                for (int i = 0; i < Alphabet.Length; i++)
                    Procent[i] = (Count[i] / AllCount) * 100;
                Sortirovka(Alphabet, Procent);

                for (int i = 0; i < Alphabet.Length; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < Words.Length; j++)
                    {
                        string letterWords = " ";
                        if ((Words[j].Length > 0)) { letterWords = Words[j].Substring(0, 1); }
                        if (Alphabet[i] == (letterWords[0]) && flag == 0)
                        {
                            Console.WriteLine(Alphabet[i]);
                            flag = 1;
                        }
                    }
                }
                return base.ToString();
            }
        }
        class Task7 : Task
        {
            private string Text1;
            private string[] Words;
            private string KeyWord;
            public Task7(string text) : base(text)
            {
                Text1 = Text.ToLower();
                Words = Text1.Split(new char[] { ' ', '"' });
                KeyWord = "фьорд";
            }
            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < Words.Length; i++)
                {
                    if (Words[i].Length >= KeyWord.Length)
                    {
                        s = Words[i].Substring(0, KeyWord.Length);
                        if (s == KeyWord)
                        {
                            Console.WriteLine(Words[i]);
                        }
                    }
                }
                return base.ToString();
            }
        }
        class Task11 : Task
        {
            public static void AlphaSort(string[] Words, char[] Alphabet)
            {
                for (int i = 0; i < Words.Length - 1; i++)
                {
                    for (int j = i + 1; j < Words.Length; j++)
                    {
                        int k = 0;
                        int flag = 0;
                        while (Words[i].Substring(k, 1) == Words[j].Substring(k, 1) && (flag == 0))
                        {
                            k++;
                            if ((k < Words[i].Length) && (k < Words[j].Length))
                            { flag = 1; }

                        }
                        if (k < Words[i].Length && k < Words[j].Length)
                        {
                            int ni = 0;
                            string si = Words[i].Substring(k, 1);
                            while (Alphabet[ni] != si[0]) { ni++; }
                            int nj = 0;
                            string sj = Words[j].Substring(k, 1);
                            while (Alphabet[nj] != sj[0]) { nj++; }
                            if (Alphabet[ni] > Alphabet[nj])
                            {
                                string aa = Words[j];
                                Words[j] = Words[i];
                                Words[i] = aa;
                            }
                        }
                    }
                }
            }
            private string Text1;
            private string[] Words;
            private char[] Alphabet = new char[33] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
            public Task11(string text) : base(text)
            {
                Text1 = Text.ToLower();
                Words = Text1.Split(new char[] { ',' });
            }
            public override string ToString()
            {
                AlphaSort(Words, Alphabet);
                for (int i = 0; i < Words.Length; i++)
                {
                    Console.WriteLine(Words[i]);
                }
                return base.ToString();
            }
        }
        class Task14 : Task
        {
            public int Sum(string text)
            {
                int sum = 0;
                foreach (char i in text)
                {
                    if (char.IsDigit(i))
                    {
                        int b = int.Parse(i.ToString());
                        sum += b;
                    }
                }
                return sum;
            }
            public Task14(string text) : base(text) { }
            public override string ToString()
            {
                int sum = Sum(base.Text);
                Console.WriteLine(sum);
                return base.ToString();
            }
        }
        static void Main(string[] args)
        {
            string text1 = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
            string text2 = "Двигатель самолета – это сложная инженерная конструкция, обеспечивающая подъем, управление и движение в воздухе. Он состоит из множества компонентов, каждый из которых играет важную роль в общей работе механизма. Внутреннее устройство двигателя включает в себя компрессор, камеру сгорания, турбину и системы управления и охлаждения. Принцип работы основан на воздушно-топливной смеси, которая подвергается сжатию, воспламенению и расширению, обеспечивая движение воздушного судна.";
            string text3 = "1 июля 2015 года Греция объявила о дефолте по государственному долгу, став первой развитой страной в истории, которая не смогла выплатить свои долговые обязательства в полном объеме. Сумма дефолта составила порядка 1,6 миллиарда евро. Этому предшествовали долгие переговоры с международными кредиторами, такими как Международный валютный фонд (МВФ), Европейский центральный банк (ЕЦБ) и Европейская комиссия (ЕК), о программах финансовой помощи и реструктуризации долга. Основными причинами дефолта стали недостаточная эффективность реформ, направленных на улучшение финансовой стабильности страны, а также политическая нестабильность, что вызвало потерю доверия со стороны международных инвесторов и кредиторов. Последствия дефолта оказались глубокими и долгосрочными: сокращение кредитного рейтинга страны, увеличение затрат на заемный капитал, рост стоимости заимствований и утрата доверия со стороны международных инвесторов.";
            string text4 = "Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. Название происходит от древнескандинавского слова \"fjǫrðr\". Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. Они популярны среди туристов и известны под разными названиями: в Норвегии – \"фьорды\", в Шотландии – \"фьордс\", в Исландии – \"фьордар\". Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.";
            string text5 = "Иванов,Петров,Смирнов,Соколов,Кузнецов,Попов,Лебедев,Волков,Козлов,Новиков,Иванова,Петрова,Смирнова";
            string text6 = "Первое кругосветное путешествие было осуществлено флотом, возглавляемым португальским исследователем Фернаном Магелланом. Путешествие началось 20 сентября 1519 года, когда флот, состоящий из пяти кораблей и примерно 270 человек, отправился из порту Сан-Лукас в Испании. Хотя Магеллан не закончил свое путешествие из-за гибели в битве на Филиппинах в 1521 году, его экспедиция стала первой, которая успешно обогнула Землю и доказала ее круглую форму. Это путешествие открыло новые морские пути и имело огромное значение для картографии и географических открытий.";

            Task1 task1 = new Task1(text1);
            Task3 task3 = new Task3(text2);
            Task5 task5 = new Task5(text3);
            Task7 task7 = new Task7(text4);
            Task11 task11 = new Task11(text5);
            Task14 task14 = new Task14(text6);

            Console.WriteLine("Задание 1:");
            task1.ToString();
            Console.WriteLine();

            Console.WriteLine("Задание 3:");
            task3.ToString();
            Console.WriteLine();

            Console.WriteLine("Задание 5:");
            task5.ToString();
            Console.WriteLine();

            Console.WriteLine("Задание 7:");
            task7.ToString();
            Console.WriteLine();

            Console.WriteLine("Задание 11:");
            task11.ToString();
            Console.WriteLine();

            Console.WriteLine("Задание 14:");
            task14.ToString();
            Console.WriteLine();
        }
    }
}
