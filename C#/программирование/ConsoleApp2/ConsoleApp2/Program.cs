using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Ввод первого числа
        Console.WriteLine("Enter the first number:");
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Error: Invalid number!");
            return;
        }

        // 2. Ввод второго числа
        Console.WriteLine("Enter the second number:");
        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Error: Invalid number!");
            return;
        }

        // 3. Ввод оператора
        Console.WriteLine("Enter the operator (&, |, or ^):");
        string? input = Console.ReadLine();

        // Проверка: оператор должен быть длиной ровно 1 символ
        if (string.IsNullOrEmpty(input) || input.Length != 1)
        {
            Console.WriteLine("Error: Invalid operator!");
            return;
        }

        char operation = input[0];
        int result = 0;
        bool isValid = true;

        // 4. Switch-case для выбора операции
        switch (operation)
        {
            case '&':
                result = a & b;
                break;
            case '|':
                result = a | b;
                break;
            case '^':
                result = a ^ b;
                break;
            default:
                Console.WriteLine("Error: Invalid operator! Use only &, |, or ^");
                return;
        }

        // 5. Вывод результата в трёх системах счисления
        Console.WriteLine($"\nResult of {a} {operation} {b} = {result}");
        Console.WriteLine($"Decimal: {result}");
        Console.WriteLine($"Binary:  {Convert.ToString(result, 2)}");
        Console.WriteLine($"Hex:     {Convert.ToString(result, 16).ToUpper()}");
    }
}