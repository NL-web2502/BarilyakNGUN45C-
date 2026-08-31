using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Core.Exceptions
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int invalidNumber, int minAllowed, int maxAllowed)
            : base($"Number {invalidNumber} is invalid. Allowed range: {minAllowed} to {maxAllowed}")
        {
        }

        public WrongDiceNumberException(int invalidNumber, int minAllowed, int maxAllowed, string message)
            : base($"{message} Number {invalidNumber} is invalid. Allowed range: {minAllowed} to {maxAllowed}")
        {
        }
    }
}
