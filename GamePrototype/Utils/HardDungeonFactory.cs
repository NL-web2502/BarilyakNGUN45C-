using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Utils
{
    public class HardDungeonFactory : DungeonFactory
    {
        private readonly UnitFactory _unitFactory;

        public HardDungeonFactory()
        {
            _unitFactory = new HardUnitFactory();
        }

        public override DungeonRoom CreateRoom(string name) => new DungeonRoom(name);

        public override DungeonRoom CreateRoomWithEnemy(string name)
        {
            var enemy = _unitFactory.CreateEnemy();
            return new DungeonRoom(name, enemy);
        }

        public override DungeonRoom CreateRoomWithLoot(string name)
        {
            var loot = CreateLoot();
            return new DungeonRoom(name, loot);
        }

        private Item CreateLoot()
        {
            Random rand = new Random();
            int lootType = rand.Next(7);

            switch (lootType)
            {
                case 0: return new Gold();
                case 1: return new HealthPotion("Health Potion");
                case 2: return new Grindstone("Grindstone");
                case 3: return new Weapon(15, 20, "Steel Sword");
                case 4: return new RangeWeapon(12, 15, 18, "Crossbow");
                case 5: return new Helmet(15, 15, "Iron Helmet");
                case 6: return new Armour(20, 15, "Chainmail");
                default: return new Gold();
            }
        }
    }
}
