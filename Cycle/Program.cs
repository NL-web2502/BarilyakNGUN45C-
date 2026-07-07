// See https://aka.ms/new-console-template for more information
namespace HomeWork_Cycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: Первые 10 чисел Фибоначчи
            Console.WriteLine("Задание 1: Первые 10 чисел Фибоначчи");
            int a = 0, b = 1;
            for (int i = 0; i < 10; i++)
            {
                Console.Write(a + " ");
                int temp = a;
                a = b;
                b = temp + b;
            }
            Console.WriteLine("\n");

            // Задание 2: Четные числа от 2 до 20
            Console.WriteLine("Задание 2: Четные числа от 2 до 20");
            for (int i = 2; i <= 20; i += 2)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("\n");

            // Задание 3: Таблица умножения от 1 до 5
            Console.WriteLine("Задание 3: Таблица умножения от 1 до 5");
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Console.Write($"{i} x {j} = {i * j}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Задание 4: Проверка пароля с использованием do-while
            Console.WriteLine("Задание 4: Проверка пароля");
            string password = "qwerty";
            string userInput;
            do
            {
                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();
                if (userInput != password)
                {
                    Console.WriteLine("Неверный пароль. Попробуйте снова.");
                }
            } while (userInput != password);
            Console.WriteLine("Пароль верный! Доступ разрешен.");
        }
    }
}
