using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class RangeWeapon : EquipItem
    {
        public uint Damage { get; }
        public uint Range { get; }

        public RangeWeapon(uint damage, uint range, uint durability, string name) : base(durability, name)
        {
            Damage = damage;
            Range = range;
        }

        public override EquipSlot Slot => EquipSlot.RangeWeapon;
    }
}
