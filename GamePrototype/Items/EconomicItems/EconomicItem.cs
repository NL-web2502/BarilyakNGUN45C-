using GamePrototype.Units;

namespace GamePrototype.Items.EconomicItems
{
    public abstract class EconomicItem : Item
    {
        public abstract class EconomicItem : Item
        {
            protected EconomicItem(string name) : base(name)
            {
            }
            public abstract void Use(Unit user);
        }
    }
}
