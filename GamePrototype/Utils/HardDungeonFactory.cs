using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;

namespace GamePrototype.Utils
{
    public sealed class HardDungeonFactory : IDungeonFactory
    {
        private readonly IUnitFactory _unitFactory;
        private readonly Random _random = Random.Shared;

        public HardDungeonFactory(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public DungeonRoom CreateRoom(string name) => new(name);

        public DungeonRoom CreateRoomWithEnemy(string name) =>
            new(name, _unitFactory.CreateEnemy());

        public DungeonRoom CreateRoomWithLoot(string name)
        {
            Item loot = name == "Final Chamber"
                ? new Weapon(20, 25, "Staff of Power")
                : CreateLoot();
            return new DungeonRoom(name, loot);
        }

        private Item CreateLoot()
        {
            return _random.Next(7) switch
            {
                0 => new Gold(),
                1 => new HealthPotion(GameConstants.HealthPotion),
                2 => new Grindstone(GameConstants.Grindstone),
                3 => new Weapon(15, 20, "Steel Sword"),
                4 => new RangeWeapon(12, 15, 18, "Crossbow"),
                5 => new Helmet(15, 15, "Iron Helmet"),
                6 => new Armour(20, 15, "Chainmail"),
                _ => new Gold()
            };
        }
    }
}