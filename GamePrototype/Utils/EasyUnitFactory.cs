using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public sealed class EasyUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);

            player.AddItemToInventory(new Weapon(10, 15, "Iron Sword"));
            player.AddItemToInventory(new Armour(10, 12, "Leather Armour"));
            player.AddItemToInventory(new Helmet(5, 10, "Leather Helmet"));
            player.AddItemToInventory(new HealthPotion(GameConstants.HealthPotion));
            player.AddItemToInventory(new Grindstone(GameConstants.Grindstone));

            return player;
        }

        public Unit CreateEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);
    }
}
