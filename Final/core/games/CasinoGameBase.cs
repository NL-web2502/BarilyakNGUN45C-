using Final.Core.Services;

namespace Final.Core.Games
{
    public abstract class CasinoGameBase
    {
        public event EventHandler<GameResultEventArgs>? OnWin;
        public event EventHandler<GameResultEventArgs>? OnLose;
        public event EventHandler<GameResultEventArgs>? OnDraw;

        public abstract void PlayGame();
        public abstract void DisplayResult();
        public abstract void SetBet(long bet);

        protected virtual void OnWinInvoke(GameResultEventArgs e)
        {
            OnWin?.Invoke(this, e);
        }

        protected virtual void OnLoseInvoke(GameResultEventArgs e)
        {
            OnLose?.Invoke(this, e);
        }

        protected virtual void OnDrawInvoke(GameResultEventArgs e)
        {
            OnDraw?.Invoke(this, e);
        }
    }
}