using Final.core.enums;
using Final.Core.Enums;

namespace Final.Core.Models
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