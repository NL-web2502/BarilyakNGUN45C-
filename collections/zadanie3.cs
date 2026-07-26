namespace коллекцииДЗ
{
    internal class Task3_LinkedList
    {
        public static void CheckTaskThird()
        {
            Console.WriteLine("Задание 3");
            {
                List<string> collection = new List<string>();
                int count;

                while (true)
                {
                    Console.Write("Введите количество элементов (от 3 до 6): ");
                    if (int.TryParse(Console.ReadLine(), out count) || count >= 3 || count <= 6)
                        break;
                }

                Console.WriteLine($"Введите {count} элемента(ов):");
                for (int i = 0; i < count; i++)
                {
                    Console.Write($"Элемент {i + 1}: ");
                    collection.Add(Console.ReadLine());
                }

                Console.WriteLine("Список в прямом порядке:");
                for (int i = 0; i < collection.Count; i++)
                {
                    Console.WriteLine(collection[i]);
                }

                Console.WriteLine("Список в обратном порядке:");
                List<string> Collection = new List<string>(collection);
                Collection.Reverse();
                foreach (string item in Collection)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
