using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public abstract class EquipItem : Item
    {
        private uint _durability;
        private uint _maxDurability;
        public uint Durability { get => _durability; protected set => _durability = value; }
        public uint MaxDurability => _maxDurability;
        public override bool Stackable => false;

        public abstract EquipSlot Slot { get; }

        protected EquipItem(uint maxDurability, string name) : base(name)
        {
            _maxDurability = maxDurability;
            _durability = maxDurability;
        }

        public bool ReduceDurability(uint delta = 1)
        {
            if (_durability == 0) return false;

            _durability -= delta;
            if (_durability < 0) _durability = 0;

            Console.WriteLine($"{Name} durability: {_durability}/{_maxDurability}");
            return _durability > 0;
        }

        public void Repair(uint delta)
        {
            if (_durability == _maxDurability)
            {
                Console.WriteLine($"{Name} is repaired");
                return;
            }
            _durability = Math.Min(_durability + delta, _maxDurability);
            Console.WriteLine($"{Name} repaired! Durability: {_durability}/{_maxDurability}");
        }

        public bool IsBroken => _durability == 0;
    }
}
    }
}
