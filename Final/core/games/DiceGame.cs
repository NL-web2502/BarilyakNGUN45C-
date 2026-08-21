using Final.core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.games
{
    public class DiceGame : CasinoGameBase
    {
        private readonly int _numberOfDice;
        private readonly int _minValue;
        private readonly int _maxValue;
        private List<Dice> _dice;
        private long _bet;
        private string _resultMessage;
        private int _playerScore;
        private int _computerScore;

        public DiceGame(int numberOfDice, int minValue, int maxValue)
        {
            if (numberOfDice < 1)
            {
                throw new ArgumentException("Number of dice must be at least 1");
            }

            if (minValue < 1 || minValue > int.MaxValue)
            {
                throw new ArgumentException("Min value must be between 1 and int.MaxValue");
            }

            if (maxValue < 1 || maxValue > int.MaxValue)
            {
                throw new ArgumentException("Max value must be between 1 and int.MaxValue");
            }

            if (minValue > maxValue)
            {
                throw new ArgumentException("Min value cannot be greater than max value");
            }

            _numberOfDice = numberOfDice;
            _minValue = minValue;
            _maxValue = maxValue;
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            _dice = new List<Dice>();
            for (int i = 0; i < _numberOfDice; i++)
            {
                _dice.Add(new Dice(_minValue, _maxValue));
            }
        }

        public void SetBet(long bet)
        {
            _bet = bet;
        }

        public override void PlayGame()
        {
            // Roll dice for player
            _playerScore = 0;
            _computerScore = 0;

            // Player's turn
            foreach (var die in _dice)
            {
                _playerScore += die.Number;
            }

            // Computer's turn - re-roll dice
            _dice.Clear();
            for (int i = 0; i < _numberOfDice; i++)
            {
                _dice.Add(new Dice(_minValue, _maxValue));
            }

            foreach (var die in _dice)
            {
                _computerScore += die.Number;
            }

            // Determine winner
            if (_playerScore > _computerScore)
            {
                _resultMessage = $"Player wins! {_playerScore} vs {_computerScore}";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet * 2));
            }
            else if (_computerScore > _playerScore)
            {
                _resultMessage = $"Computer wins! {_computerScore} vs {_playerScore}";
                OnLooseInvoke(new GameResultEventArgs(_resultMessage, _bet, 0));
            }
            else
            {
                _resultMessage = $"It's a draw! Both have {_playerScore} points!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet));
            }

            DisplayResult();
        }

        public override void DisplayResult()
        {
            Console.WriteLine("\n===== DICE GAME RESULT =====");
            Console.WriteLine($"Player's total: {_playerScore}");
            Console.WriteLine($"Computer's total: {_computerScore}");
            Console.WriteLine($"Result: {_resultMessage}");
            Console.WriteLine("===========================\n");
        }
    }
}
