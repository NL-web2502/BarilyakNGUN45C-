using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 1 - Числа Фибоначчи (первые 10 чисел: 0, 1, 1, 2, 3, 5, 8, 13)
            int[] fibonacci = new int[8];
            fibonacci[0] = 0;
            fibonacci[1] = 1;
            for (int i = 2; i < fibonacci.Length; i++)
            {
                fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
            }
            Console.WriteLine("Задание 1 - Числа Фибоначчи:");
            foreach (int num in fibonacci)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine("\n");


        }
    }
}