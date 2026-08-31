namespace Final.Core.Services
{
    public class GameResultEventArgs : EventArgs
    {
        public string Message { get; }
        public long BetAmount { get; }
        public GameResult Result { get; }

        public GameResultEventArgs(string message, long betAmount, GameResult result)
        {
            Message = message;
            BetAmount = betAmount;
            Result = result;
        }
    }

    public enum GameResult
    {
        Win,
        Lose,
        Draw
    }
}