using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.services
{
    public class GameResultEventArgs : EventArgs
    {
        public string Message { get; }
        public long BetAmount { get; }
        public long ResultAmount { get; }

        public GameResultEventArgs(string message, long betAmount, long resultAmount)
        {
            Message = message;
            BetAmount = betAmount;
            ResultAmount = resultAmount;
        }
    }
}
