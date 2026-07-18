using System;

namespace RPG
{
    // Структура Interval для определения границ интервала чисел с плавающей точкой
    public struct Interval
    {
        private float _min;
        private float _max;
        private static Random _random = new Random();

        // Открытые свойства
        public float Min => _min;
        public float Max => _max;

        // Конструктор с двумя аргументами типа int
        public Interval(int minValue, int maxValue)
        {
            // Проверка на отрицательные значения
            int tempMin = minValue < 0 ? 0 : minValue;
            int tempMax = maxValue < 0 ? 0 : maxValue;

            if (minValue < 0 || maxValue < 0)
            {
                Console.WriteLine("Предупреждение: Отрицательное значение заменено на 0");
            }

            // Проверка на корректность интервала
            if (tempMin > tempMax)
            {
                Console.WriteLine("Предупреждение: Min больше Max, значения поменяны местами");
                int temp = tempMin;
                tempMin = tempMax;
                tempMax = temp;
            }

            // Если оба числа равны, увеличиваем максимальное на 10
            if (tempMin == tempMax)
            {
                Console.WriteLine("Предупреждение: Min равен Max, Max увеличен на 10");
                tempMax += 10;
            }

            _min = tempMin;
            _max = tempMax;
        }
        public float Get()
        {
            return (float)(_random.NextDouble() * (_max - _min) + _min);
        }

        public override string ToString()
        {
            return $"[{_min:F2}; {_max:F2}]";
        }
    }

    // Класс Unit (юнит)
    public class Unit
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Armor { get; private set; }
        public Interval Damage { get; private set; }

        // Конструктор с параметрами для имени и здоровья
        public Unit(string name, int health, int armor = 0)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = new Interval(0, 10); // По умолчанию урон 0-10
        }

        // Конструктор с параметрами для имени, здоровья и урона
        public Unit(string name, int health, int minDamage, int maxDamage, int armor = 0)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = new Interval(minDamage, maxDamage);
        }

        // Получение урона
        public int GetDamage()
        {
            return (int)Math.Round(Damage.Get());
        }

        public override string ToString()
        {
            return $"Unit: {Name}, Health: {Health}, Armor: {Armor}, Damage: {Damage}";
        }
    }

    // Класс Weapon (оружие)
    public class Weapon
    {
        public string Name { get; private set; }
        public int Durability { get; private set; }
        public Interval Damage { get; private set; }

        // Конструктор с параметрами для имени и прочности
        public Weapon(string name, int durability = 100)
        {
            Name = name;
            Durability = durability;
            Damage = new Interval(5, 15); // По умолчанию урон 5-15
        }

        // Конструктор с параметрами для имени, прочности и урона
        public Weapon(string name, int minDamage, int maxDamage, int durability = 100)
        {
            Name = name;
            Durability = durability;
            Damage = new Interval(minDamage, maxDamage);
        }

        // Метод для получения урона
        public int GetDamage()
        {
            return (int)Math.Round(Damage.Get());
        }
        public override string ToString()
        {
            return $"Weapon: {Name}, Durability: {Durability}, Damage: {Damage}";
        }
    }

    // Структура Room
    public struct Room
    {
        public Unit Unit { get; private set; }
        public Weapon Weapon { get; private set; }

        public Room(Unit unit, Weapon weapon)
        {
            Unit = unit;
            Weapon = weapon;
        }
    }

    // Класс Dungeon 
    public class Dungeon
    {
        private Room[] _rooms;
                
        public Dungeon()
        {
            // массив
            _rooms = new Room[]
            {
                new Room(
                    new Unit("Warrior", 100, 10, 20, 15),
                    new Weapon("Axe", 15, 25, 100)
                ),
                new Room(
                    new Unit("Mage", 80, 5, 15, 5),
                    new Weapon("Staff", 10, 20, 80)
                ),
                new Room(
                    new Unit("Archer", 90, 8, 18, 10),
                    new Weapon("Bow", 12, 22, 90)
                ),
                new Room(
                    new Unit("Knight", 120, 12, 22, 20),
                    new Weapon("Sword", 18, 30, 70)
                )
            };
        }

        // ShowRooms
        public void ShowRooms()
        {
            Console.WriteLine("=== Подземелье ===");
            Console.WriteLine($"Количество комнат: {_rooms.Length}\n");

            for (int i = 0; i < _rooms.Length; i++)
            {
                var room = _rooms[i];
                Console.WriteLine($"Комната {i + 1}:");
                Console.WriteLine($"  {room.Unit}");
                Console.WriteLine($"  {room.Weapon}");

                // Дополнительная информация
                Console.WriteLine($"  Фактический урон юнита: {room.Unit.GetDamage()}");
                Console.WriteLine($"  Фактический урон оружия: {room.Weapon.GetDamage()}");
                Console.WriteLine("---");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Тестирование структуры Interval
            Console.WriteLine("=== Тестирование Interval ===");
            var interval1 = new Interval(5, 15);
            Console.WriteLine($"Интервал 1: {interval1}");

            var interval2 = new Interval(20, 10); // Минимальное больше максимального
            Console.WriteLine($"Интервал 2: {interval2}");

            var interval3 = new Interval(-5, 10); // Отрицательное значение
            Console.WriteLine($"Интервал 3: {interval3}");

            var interval4 = new Interval(8, 8); // Равные значения
            Console.WriteLine($"Интервал 4: {interval4}");

            Console.WriteLine("\nСлучайные значения из интервалов:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"  {interval1.Get():F2}");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Создание экземпляра Dungeon
            var dungeon = new Dungeon();

            // Вызов метода ShowRooms
            dungeon.ShowRooms();

            // Дополнительное тестирование
            Console.WriteLine("\n=== Дополнительное тестирование ===");
            var unit = new Unit("TestUnit", 100, 3, 7);
            var weapon = new Weapon("TestWeapon", 2, 6);
            var room = new Room(unit, weapon);

            Console.WriteLine($"Юнит: {unit.Name}, Урон: {unit.GetDamage()}");
            Console.WriteLine($"Оружие: {weapon.Name}, Урон: {weapon.GetDamage()}");
        }
    }
}