using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите задание:");
            Console.WriteLine("1 - Работа со списком");
            Console.WriteLine("2 - Работа со студентами");
            Console.WriteLine("3 - Работа с двойными списками");

            Console.Write("Ваш выбор: ");

            if (int.TryParse(Console.ReadLine(), out int task))
            {
                switch (task)
                {
                    case 1:
                        Task1List.CheckTaskFirst();
                        break;

                    case 2:
                        Task2Dictionary.CheckTaskSecond();
                        break;

                    case 3:
                        Task3LinkedList.CheckTaskThird();
                        break;

                    default:
                        Console.WriteLine("Неверный номер задания!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода!");
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
               
}
