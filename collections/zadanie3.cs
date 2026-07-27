using System.Xml.Linq;

namespace CollectionHomework
{
    internal class Task3LinkedList
    {
        public static void CheckTaskThird()
        {
            LinkedListTask list = new LinkedListTask();
            list.TaskLoop();
        }

        public class Node
        {
            public string Data { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }

            public Node(string data)
            {
                Data = data;
                Next = null;
                Prev = null;
            }
        }
        public class LinkedListTask
        {
            private Node _head;
            private Node _tail;
            private int _count;

            public LinkedListTask()
            {
                _head = null;
                _tail = null;
                _count = 0;
            }

            public void Add(string data)
            {
                Node newNode = new Node(data);

                if (_head == null)
                {
                    _head = newNode;
                    _tail = newNode;
                }
                else
                {
                    _tail.Next = newNode;
                    newNode.Prev = _tail;
                    _tail = newNode;
                }

                _count++;
            }

            public void PrintForward()
            {
                if (_head == null)
                {
                    Console.WriteLine("Список пуст.");
                    return;
                }

                Console.WriteLine("Список в прямом порядке:");
                Node current = _head;
                int index = 1;
                while (current != null)
                {
                    Console.WriteLine($"{index}. {current.Data}");
                    current = current.Next;
                    index++;
                }
            }

            public void PrintBackward()
            {
                if (_tail == null)
                {
                    Console.WriteLine("Список пуст.");
                    return;
                }

                Console.WriteLine("Список в обратном порядке:");
                Node current = _tail;
                int index = _count;
                while (current != null)
                {
                    Console.WriteLine($"{index}. {current.Data}");
                    current = current.Prev;
                    index--;
                }
            }

            public void TaskLoop()
            {
                Console.WriteLine("\n=== ЗАДАНИЕ 3: Собственный двусвязный список ===");

                int count = 0;
                while (true)
                {
                    Console.Write("Введите количество элементов (от 3 до 6): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out count) && count >= 3 && count <= 6)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Введите число от 3 до 6.");
                    }
                }

                Console.WriteLine($"Введите {count} элемента(ов):");
                for (int i = 0; i < count; i++)
                {
                    Console.Write($"Элемент {i + 1}: ");
                    string data = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(data))
                    {
                        data = "Пусто";
                    }

                    Add(data);
                }

                Console.WriteLine($"\nВсего добавлено элементов: {_count}");
                PrintForward();
                PrintBackward();

                Console.WriteLine("\nНажмите Enter для возврата в меню...");
                Console.ReadLine();
            }
        }

      }
    }
