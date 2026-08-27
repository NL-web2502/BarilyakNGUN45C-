using GamePrototype.Dungeon;

namespace GamePrototype.Utils
{
    public sealed class DungeonBuilder
    {
        private readonly IDungeonFactory _factory;

        public DungeonBuilder(IDungeonFactory factory)
        {
            _factory = factory;
        }

        public DungeonRoom BuildDungeon()
        {
            var enter = _factory.CreateRoom("Entrance Hall");
            var monsterRoom = _factory.CreateRoomWithEnemy("Goblin Den");
            var emptyRoom = _factory.CreateRoom("Empty Corridor");
            var lootRoom = _factory.CreateRoomWithLoot("Treasure Chamber");
            var potionRoom = _factory.CreateRoomWithLoot("Alchemy Lab");
            var weaponRoom = _factory.CreateRoomWithLoot("Armory");
            var helmetRoom = _factory.CreateRoomWithLoot("Guard Room");
            var finalRoom = _factory.CreateRoomWithLoot("Final Chamber");

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
}
