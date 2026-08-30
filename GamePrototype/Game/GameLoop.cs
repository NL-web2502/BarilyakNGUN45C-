using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private readonly IUnitFactory _unitFactory;
        private readonly DungeonBuilder _dungeonBuilder;
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new();

        public GameLoop(IUnitFactory unitFactory, DungeonBuilder dungeonBuilder)
        {
            _unitFactory = unitFactory;
            _dungeonBuilder = dungeonBuilder;
        }

        public void StartGame()
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        private void Initialize()
        {
            _dungeon = _dungeonBuilder.BuildDungeon();
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

            while (!currentRoom.IsFinal)
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success)
                {
                    Console.WriteLine("Game over!");
                    return;
                }

                if (_player is Player player)
                {
                    Console.WriteLine("Do you want to use a Grindstone? (y/n)");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        player.UseEconomicItem(GameConstants.Grindstone);
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

                    Console.WriteLine("Wrong direction! Choose from available directions:");
                    DisplayRouteOptions(currentRoom);
                }
            }

            Console.WriteLine($"Congratulations, {_player.Name}!");
            Console.WriteLine("Final result:");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null)
            {
                Console.WriteLine($"Found {currentRoom.Loot.Name}!");
                _player.AddItemToInventory(currentRoom.Loot);
            }

            if (currentRoom.Enemy != null)
            {
                Console.WriteLine($"Enemy: {currentRoom.Enemy.Name}!");
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    _player.AddItemsFromUnitToInventory(currentRoom.Enemy);
                    Console.WriteLine($"Defeated {currentRoom.Enemy.Name}!");
                }
                else
                {
                    success = false;
                }
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int)room.Key}	");
            }
            Console.WriteLine();
        }
    }
}
