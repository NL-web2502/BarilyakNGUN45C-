using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);

            player.AddItemToInventory(new Weapon(10, 15, "Стальной меч"));

            player.AddItemToInventory(new Armour(15, 12, "Кольчуга"));

            player.AddItemToInventory(new Helmet(8, 10, "Стальной шлем"));

            player.AddItemToInventory(new RangeWeapon(5, 10, 8, "Лук"));

            player.AddItemToInventory(new HealthPotion("Зелье здоровья"));

            player.AddItemToInventory(new Grindstone("Точильный камень"));

            return player;
        }

        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);
    }
}
