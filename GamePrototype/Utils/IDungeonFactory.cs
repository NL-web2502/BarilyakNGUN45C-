using GamePrototype.Dungeon;

namespace GamePrototype.Utils
{
    public interface IDungeonFactory
    {
        DungeonRoom CreateRoom(string name);
        DungeonRoom CreateRoomWithEnemy(string name);
        DungeonRoom CreateRoomWithLoot(string name);
    }
}
