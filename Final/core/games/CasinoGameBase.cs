using Final.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.games
{
    public abstract class CasinoGameBase
    {
        public event EventHandler<GameResultEventArgs> OnWin;
        public event EventHandler<GameResultEventArgs> OnLoose;
        public event EventHandler<GameResultEventArgs> OnDraw;

        public abstract void PlayGame();

        protected virtual void OnWinInvoke(GameResultEventArgs e)
        {
            OnWin?.Invoke(this, e);
        }

        protected virtual void OnLooseInvoke(GameResultEventArgs e)
        {
            OnLoose?.Invoke(this, e);
        }

        protected virtual void OnDrawInvoke(GameResultEventArgs e)
        {
            OnDraw?.Invoke(this, e);
        }

        protected abstract void FactoryMethod();

        public abstract void DisplayResult();

        public abstract void SetBet(long bet);
    }
}
