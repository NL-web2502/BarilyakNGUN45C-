using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 6 - Работа с массивами
            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

            // Задание 6 - Изменение размера первого массива в 2 раза
            int newSize = array.Length * 2;
            Array.Resize(ref array, newSize);
            Console.WriteLine($"Задание 6 - array после изменения размера (теперь {array.Length} элементов):");
            foreach (int num in array)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();


        }
    }
}