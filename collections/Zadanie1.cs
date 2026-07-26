using System;
using System.Collections;
using System.Collections.Generic;
namespace коллекцииДЗ
{
    internal class Task1_List
    {
        public static void CheckTaskFirst()
        {
            Console.WriteLine("Задание 1");
        }   
        private class ListTask
        {
            private readonly List<string> numbers;

            public ListTask() 
            {
                numbers = new List<string> { "Один", "Два", "Три", "Четыре" };
            }

            public void TaskLoop()
            {
                Console.WriteLine("ЗАДАНИЕ 1: Работа со списком");
                Console.WriteLine("Текущий список:");
                PrintList();

                Console.Write("Введите новую строку для добавления в конец списка: ");
                string newItem = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newItem))
                {
                    numbers.Add(newItem);
                    Console.WriteLine($"Строка '{newItem}' добавлена в конец.");
                }

                Console.WriteLine("Список после добавления:");
                PrintList();


                Console.Write("Введите строку для добавления в середину списка:");
                string middleItem = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(middleItem))
                {
                    int middleIndex = numbers.Count / 2;
                    numbers.Insert(middleIndex, middleItem);
                    Console.WriteLine($"Строка '{middleItem}' добавлена в середину (позиция {middleIndex + 1}).");
                }

                Console.WriteLine("Итоговый список:");
                PrintList();

                Console.WriteLine("Для выхода из задачи введите 'exit'");
                while (Console.ReadLine() != "exit") { }
                ;
            }

            private void PrintList()
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {numbers[i]}");
                }
            }
        }
    }
} 


