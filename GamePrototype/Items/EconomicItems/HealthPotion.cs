using GamePrototype.Units;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class HealthPotion : EconomicItem
    {
        public uint HealthRestore => 7;
        public override bool Stackable => false;

        public HealthPotion(string name) : base(name)
        {
        }

        public override void Use(Unit user)
        {
            user.Health = Math.Min(user.Health + HealthRestore, user.MaxHealth);
            Console.WriteLine($"{user.Name} used a Health Potion! Health: {user.Health}/{user.MaxHealth}");
        }
    }
}
}
