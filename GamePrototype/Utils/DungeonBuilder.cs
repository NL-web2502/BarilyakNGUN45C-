using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;

namespace GamePrototype.Utils
{
    public static class DungeonBuilder
    {
        public static DungeonRoom BuildDungeon(Difficulty difficulty = Difficulty.Easy)
        {
            DungeonFactory factory = difficulty == Difficulty.Easy
                ? new EasyDungeonFactory()
                : new HardDungeonFactory();

            var enter = factory.CreateRoom("Entrance Hall");
            var monsterRoom = factory.CreateRoomWithEnemy("Goblin Den");
            var emptyRoom = factory.CreateRoom("Empty Corridor");
            var lootRoom = factory.CreateRoomWithLoot("Treasure Chamber");
            var potionRoom = factory.CreateRoomWithLoot("Alchemy Lab");
            var weaponRoom = factory.CreateRoomWithLoot("Armory");
            var helmetRoom = factory.CreateRoomWithLoot("Guard Room");
            var finalRoom = factory.CreateRoom("Final Chamber");
             
            if (difficulty == Difficulty.Hard)
            {
                var finalLoot = new Items.EquipItems.MagicWeapon(20, 15, 10, 25, "Staff of Power");
                finalRoom = new DungeonRoom("Final Chamber", finalLoot);
            }
            else
            {
                var finalLoot = new Items.EconomicItems.Gold();
                finalRoom = new DungeonRoom("Final Chamber", finalLoot);
            }
             
            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, potionRoom);

            emptyRoom.TrySetDirection(Direction.Forward, weaponRoom);
            emptyRoom.TrySetDirection(Direction.Right, helmetRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootRoom.TrySetDirection(Direction.Right, finalRoom);

            potionRoom.TrySetDirection(Direction.Forward, finalRoom);
            weaponRoom.TrySetDirection(Direction.Forward, finalRoom);
            helmetRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }

    public enum Difficulty
    {
        Easy,
        Hard
    }
}
