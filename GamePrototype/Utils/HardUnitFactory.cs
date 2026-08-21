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
    public class HardUnitFactory : UnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);

            player.AddItemToInventory(new Weapon(15, 20, "Steel Sword"));
            player.AddItemToInventory(new Armour(20, 15, "Chainmail"));
            player.AddItemToInventory(new Helmet(10, 15, "Steel Helmet"));
            player.AddItemToInventory(new RangeWeapon(8, 10, 12, "Crossbow"));
            player.AddItemToInventory(new HealthPotion("Health Potion"));
            player.AddItemToInventory(new HealthPotion("Health Potion"));
            player.AddItemToInventory(new Grindstone("Grindstone"));
            player.AddItemToInventory(new Grindstone("Grindstone"));

            return player;
        }

        public Unit CreateEnemy()
        {
            var enemy = new Goblin("Orc Warlord", 35, 35, 5);

            var weapon = new Weapon(15, 20, "Orc Axe");
            enemy.AddItemToInventory(weapon);

            var armour = new Armour(20, 15, "Orc Armour");
            enemy.AddItemToInventory(armour);

            var helmet = new Helmet(10, 12, "Orc Helmet");
            enemy.AddItemToInventory(helmet);

            return enemy;
        }
    }
}
