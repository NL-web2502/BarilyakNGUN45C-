using System;
using System.Text;

namespace String_symbol
{
    internal class Program
    {
        static void Main()
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
        }
        // задание 1
        static void Task1()
        {
            string result = ConcatenateStrings("Hello, ", "World!");
            Console.WriteLine(result);

            string result2 = ConcatenateStrings("Buy", "");
            Console.WriteLine(result2);
        }
        static string ConcatenateStrings(string str1, string str2)
        {
            return (str1 + str2);
        }

        // задание2
        static void Task2()
        {
            string greetResult = GreetUser("Nick", 30);
            Console.WriteLine(greetResult);

        }
        static string GreetUser(string name, int age)
        {
            return $"Hello, {name}! You are {age} years old. \nNice to meet you!";
        }

        // задание3
        static void Task3()
        {
            string Result1 = AnalyzeString("Aerosmith is crazy");
            string Result2 = AnalyzeString("RocK`n roll!");
            Console.WriteLine(Result1);
            Console.WriteLine(Result2);
        }
        static string AnalyzeString(string input)
        {
            int length = input.Length;
            string upper = input.ToUpper();
            string lower = input.ToLower();
            return $"Длина; {length}, верхняя строка:{upper}, нижняя строка {lower}";
        }


        // задание 4
        static void Task4()
        {
            string result = GetFirstFiveChars("Naturwissenschaften");
            Console.WriteLine(result);
        }
        static string GetFirstFiveChars(string input)
        {
                   
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length < 5)
                return input;

            return input.Substring(0, 5);
        }

        // задание 5
        static void Task5()
        {
            string[] words = { "Need", "for", "speed" };
            StringBuilder result = BuildSentenceFromArray(words);
            Console.WriteLine(result.ToString());

            string[] words2 = { "C#", "is", "awesome", "language" };
            StringBuilder result2 = BuildSentenceFromArray(words2);
            Console.WriteLine(result2.ToString());

            string[] words3 = { "HeHe" };
            StringBuilder result3 = BuildSentenceFromArray(words3);
            Console.WriteLine(result3.ToString());
        }
        static StringBuilder BuildSentenceFromArray(string[] words)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                sb.Append(words[i]);
                if (i < words.Length - 1)
                    sb.Append(" ");
            }

            return sb;
        }

        // задание 6

        static void Task6()
        {
            string result = ReplaceWords("Hello world", "world", "universe");
            Console.WriteLine(result);
        }
        static string ReplaceWords(string inputString, string wordToReplace, string replacementWord)
        {
            return inputString.Replace(wordToReplace, replacementWord);
        }
    }
}


