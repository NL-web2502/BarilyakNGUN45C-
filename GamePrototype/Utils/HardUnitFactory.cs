using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public sealed class HardUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);

            player.AddItemToInventory(new Weapon(15, 20, "Steel Sword"));
            player.AddItemToInventory(new Armour(20, 15, "Chainmail"));
            player.AddItemToInventory(new Helmet(10, 15, "Steel Helmet"));
            player.AddItemToInventory(new RangeWeapon(8, 10, 12, "Crossbow"));
            player.AddItemToInventory(new HealthPotion(GameConstants.HealthPotion));
            player.AddItemToInventory(new Grindstone(GameConstants.Grindstone));
            player.AddItemToInventory(new Grindstone(GameConstants.Grindstone));

            return player;
        }

        public Unit CreateEnemy()
        {
            var enemy = new Goblin(GameConstants.OrcWarlord, 35, 35, 5);
            enemy.AddItemToInventory(new Weapon(15, 20, "Orc Axe"));
            enemy.AddItemToInventory(new Armour(20, 15, "Orc Armour"));
            enemy.AddItemToInventory(new Helmet(10, 12, "Orc Helmet"));
            return enemy;
        }
    }
}
