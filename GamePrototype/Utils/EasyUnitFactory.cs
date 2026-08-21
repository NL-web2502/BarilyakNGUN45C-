using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Utils
{
    public class EasyUnitFactory : UnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
             
            player.AddItemToInventory(new Weapon(10, 15, "Iron Sword"));
            player.AddItemToInventory(new Armour(10, 12, "Leather Armour"));
            player.AddItemToInventory(new Helmet(5, 10, "Leather Helmet"));
            player.AddItemToInventory(new HealthPotion("Health Potion"));
            player.AddItemToInventory(new Grindstone("Grindstone"));

            return player;
        }

        public Unit CreateEnemy()
        {
            var enemy = new Goblin("Goblin", 18, 18, 2);
            return enemy;
        }
    }
}
