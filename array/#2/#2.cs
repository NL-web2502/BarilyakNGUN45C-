using System;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 2 - Названия месяцев на английском
            string[] months = new string[12]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };
            Console.WriteLine("Задание 2 - Месяцы:");
            foreach (string month in months)
            {
                Console.Write(month + " ");
            }
            Console.WriteLine("\n");


        }
    }
}