using Final.core.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.models
{
    public readonly struct Card
    {
        public Suit Suit { get; }
        public CardRank Rank { get; }

        public Card(Suit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int GetValue()
        {
            return (int)Rank;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
