using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.models
{
    public class PlayerProfile
    {
        public string Name { get; set; }
        public long Bank { get; set; }

        public PlayerProfile()
        {
            Name = string.Empty;
            Bank = 1000;
        }

        public PlayerProfile(string name, long bank = 1000)
        {
            Name = name;
            Bank = bank;
        }
    }
}
