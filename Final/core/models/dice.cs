using Final.Core.Exceptions;

namespace Final.Core.Models
{
    public readonly struct Dice
    {
        private static readonly Random _random = new Random();

        public int Number { get; }

        public Dice(int min, int max)
        {
            if (min < 1)
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            if (max < 1)
                throw new WrongDiceNumberException(max, 1, int.MaxValue);
            if (min > max)
                throw new WrongDiceNumberException(min, min, max, "Min value cannot be greater than max value.");

            Number = _random.Next(min, max + 1);
        }

        public override string ToString() => Number.ToString();
    }
}