using Final.core.exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.models
{
    public readonly struct Dice
    {
        private readonly int _min;
        private readonly int _max;
        private static readonly Random _random = new Random();

        public int Number { get; }

        public Dice(int min, int max)
        { 
            if (min < 1)
            {
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            }

            if (max < 1)
            {
                throw new WrongDiceNumberException(max, 1, int.MaxValue);
            }

            if (min > max)
            {
                throw new WrongDiceNumberException(min, min, max, "Min value cannot be greater than max value.");
            }

            _min = min;
            _max = max;
            Number = _random.Next(min, max + 1);
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
