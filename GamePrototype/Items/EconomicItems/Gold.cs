using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class Gold : EconomicItem
    {
        public override bool Stackable => true;

        public Gold() : base(GameConstants.Gold)
        {
        }

        public override void Use(Unit user)
        {
            Console.WriteLine($"{user.Name} collected Gold!"); 
        }
    }
}
