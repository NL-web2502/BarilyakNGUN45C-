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
    public class EasyDungeonFactory : DungeonFactory
    {
        private readonly UnitFactory _unitFactory;

        public EasyDungeonFactory()
        {
            _unitFactory = new EasyUnitFactory();
        }

        public DungeonRoom CreateRoom(string name) => new DungeonRoom(name);

        public DungeonRoom CreateRoomWithEnemy(string name)
        {
            var enemy = _unitFactory.CreateEnemy();
            return new DungeonRoom(name, enemy);
        }

        public DungeonRoom CreateRoomWithLoot(string name)
        {
            var loot = CreateLoot();
            return new DungeonRoom(name, loot);
        }

        private Item CreateLoot()
        {
            Random rand = new Random();
            int lootType = rand.Next(4);

            switch (lootType)
            {
                case 0: return new Gold();
                case 1: return new HealthPotion("Health Potion");
                case 2: return new Grindstone("Grindstone");
                case 3: return new Weapon(10, 15, "Iron Sword");
                default: return new Gold();
            }
        }
    }
}
