using GamePrototype.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Utils
{
    public interface UnitFactory
    {
        Unit CreatePlayer(string name);
        Unit CreateEnemy();
    }

    public interface DungeonFactory
    {
        Dungeon.DungeonRoom CreateRoom(string name);
        Dungeon.DungeonRoom CreateRoomWithEnemy(string name);
        Dungeon.DungeonRoom CreateRoomWithLoot(string name);
    }
}
