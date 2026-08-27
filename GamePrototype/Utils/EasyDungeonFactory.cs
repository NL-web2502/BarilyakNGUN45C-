using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;

namespace GamePrototype.Utils
{
    public sealed class EasyDungeonFactory : IDungeonFactory
    {
        private readonly IUnitFactory _unitFactory;
        private readonly Random _random = Random.Shared;

        public EasyDungeonFactory(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public DungeonRoom CreateRoom(string name) => new(name);

        public DungeonRoom CreateRoomWithEnemy(string name) =>
            new(name, _unitFactory.CreateEnemy());

        public DungeonRoom CreateRoomWithLoot(string name)
        {
            Item loot = name == "Final Chamber" ? new Gold() : CreateLoot();
            return new DungeonRoom(name, loot);
        }

        private Item CreateLoot()
        {
            return _random.Next(4) switch
            {
                0 => new Gold(),
                1 => new HealthPotion(GameConstants.HealthPotion),
                2 => new Grindstone(GameConstants.Grindstone),
                3 => new Weapon(10, 15, "Iron Sword"),
                _ => new Gold()
            };
        }
    }
}
