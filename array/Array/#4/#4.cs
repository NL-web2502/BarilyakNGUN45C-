using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 4 - Jagged array (ломаный массив)
            double[][] jaggedArray = new double[3][];
            // Первый массив - числа от 1 до 5
            jaggedArray[0] = new double[5] { 1, 2, 3, 4, 5 };
            // Второй массив - константы e и pi
            jaggedArray[1] = new double[2] { Math.E, Math.PI };
            // Третий массив - логарифмы по основанию 10 чисел 1, 10, 100, 1000
            jaggedArray[2] = new double[4]
            {
                Math.Log10(1),
                Math.Log10(10),
                Math.Log10(100),
                Math.Log10(1000)
            };

            Console.WriteLine("Задание 4 - Jagged array:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.Write($"Массив {i + 1}: ");
                foreach (double num in jaggedArray[i])
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


        }
    }
}