using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        private readonly Difficulty _difficulty;
        private readonly UnitFactory _unitFactory;

        public GameLoop(Difficulty difficulty)
        {
            _difficulty = difficulty;
            _unitFactory = difficulty == Difficulty.Easy
                ? new EasyUnitFactory()
                : new HardUnitFactory();
        }

        public void StartGame()
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            _dungeon = DungeonBuilder.BuildDungeon(_difficulty);
            Console.WriteLine("Enter your name");
            _player = _unitFactory.CreatePlayer(Console.ReadLine());
            Console.WriteLine($"Hello {_player.Name}");

            if (_player is Player player)
            {
                player.ShowEquipmentStatus();
            }
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;

            while (currentRoom.IsFinal == false)
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success)
                {
                    Console.WriteLine("Game over!");
                    return;
                }

                if (_player is Player player)
                {
                    Console.WriteLine("\nDo you want to use a Grindstone? (y/n)");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        player.UseGrindstone();
                        player.ShowEquipmentStatus();
                    }
                }

                DisplayRouteOptions(currentRoom);
                while (true)
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction) && currentRoom.Rooms.ContainsKey(direction))
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong direction! Choose from available directions:");
                        DisplayRouteOptions(currentRoom);
                    }
                }
            }
            Console.WriteLine($"\nCongratulations, {_player.Name}!");
            Console.WriteLine("Final result:");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null)
            {
                Console.WriteLine($"Found {currentRoom.Loot.Name}!");

                if (_player is Player player)
                {
                    player.AddItemToInventory(currentRoom.Loot);
                }
                else
                {
                    _player.AddItemToInventory(currentRoom.Loot);
                }
            }
            if (currentRoom.Enemy != null)
            {
                Console.WriteLine($"\nEnemy: {currentRoom.Enemy.Name}!");
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
                Console.WriteLine($"Defeated {enemy.Name}!");
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("\nWhere to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int)room.Key}\t");
            }
            Console.WriteLine();
        }

        #endregion
    }
}