using System;
using System.Collections.Generic;

namespace HomeWork.Task2
{
    internal class Program
    {
        private class DictionaryTask
        {
            private readonly Dictionary<string, double> _studentGrades;

            public DictionaryTask()
            {
                _studentGrades = new Dictionary<string, double>();
            }

            public void TaskLoop()
            {
                Console.WriteLine("ЗАДАНИЕ 2:");

                while (true)
                {
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1 - Добавить студента");
                    Console.WriteLine("2 - Найти оценку студента");
                    Console.WriteLine("3 - Показать всех студентов");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();

                    if (choice == "exit")
                    {
                        Console.WriteLine("Выход из задачи");
                        break;
                    }

                    switch (choice)
                    {
                        case "1":
                            AddStudent();
                            break;
                        case "2":
                            FindStudent();
                            break;
                        case "3":
                            ShowAllStudents();
                            break;
                        default:
                            Console.WriteLine("Неверный ввод. Попробуйте снова.");
                            break;
                    }
                }
            }

            private void AddStudent()
            {
                Console.Write("Введите имя студента: ");
                string name = Console.ReadLine();

                Console.Write("Введите оценку (от 2 до 5): ");
                if (!double.TryParse(Console.ReadLine(), out double grade) || grade < 2 || grade > 5)
                {
                    Console.WriteLine("Ошибка! Оценка должна быть числом от 2 до 5.");
                    return;
                }
                _studentGrades[name] = grade;
                Console.WriteLine($"Студент '{name}' с оценкой {grade} добавлен/обновлен.");
            }

            private void FindStudent()
            {
                Console.Write("Введите имя студента для поиска: ");
                string name = Console.ReadLine();

                if (_studentGrades.TryGetValue(name, out double grade))
                {
                    Console.WriteLine($"Студент: {name}, Средняя оценка: {grade:F2}");
                }
                else
                {
                    Console.WriteLine($"Студента с именем '{name}' не существует.");
                }
            }

            private void ShowAllStudents()
            {
                if (_studentGrades.Count == 0)
                {
                    Console.WriteLine("Список студентов пуст.");
                    return;
                }

                Console.WriteLine("Список всех студентов:");
                foreach (var student in _studentGrades)
                {
                    Console.WriteLine($"{student.Key}: {student.Value:F2}");
                }
            }
        }
    }
}

         