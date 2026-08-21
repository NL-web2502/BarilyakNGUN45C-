using GamePrototype.Units;

namespace GamePrototype.Items.EconomicItems
{
     public sealed class Grindstone : EconomicItem
      {
            public override bool Stackable => false;

            public Grindstone(string name) : base(name)
            {
            }

            public override void Use(Unit user)
            {
                if (user is Player player)
                {
                    player.UseGrindstone();
                }
                else
                {
                    Console.WriteLine("Grindstone can only be used by Player");
                }
            }
        }
    }
