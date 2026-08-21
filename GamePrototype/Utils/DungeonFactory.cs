using GamePrototype.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Utils
{
    public abstract class DungeonFactory
    {
        public abstract DungeonRoom CreateRoom(string name);
        public abstract DungeonRoom CreateRoomWithEnemy(string name);
        public abstract DungeonRoom CreateRoomWithLoot(string name);
    }
}
