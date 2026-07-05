using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 5 и 6 - Работа с массивами
            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

            // Задание 5 - Копирование первых 3 элементов первого массива во второй
            Array.Copy(array, 0, array2, 0, 3);
            Console.WriteLine("Задание 5 - array2 после копирования первых 3 элементов из array:");
            foreach (int num in array2)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine("\n");


        }
    }
}