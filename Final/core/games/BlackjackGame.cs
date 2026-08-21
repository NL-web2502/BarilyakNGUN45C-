using Final.core.enums;
using Final.core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.core.games
{
    public class BlackjackGame : CasinoGameBase
    {
        private readonly int _numberOfCards;
        private Queue<Card> _deck;
        private List<Card> _playerCards;
        private List<Card> _computerCards;
        private long _bet;
        private string _resultMessage;

        public BlackjackGame(int numberOfCards)
        {
            if (numberOfCards < 10 || numberOfCards > 52)
            {
                throw new ArgumentException("Number of cards must be between 10 and 52");
            }

            _numberOfCards = numberOfCards;
            _playerCards = new List<Card>();
            _computerCards = new List<Card>();
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            _deck = new Queue<Card>();
            List<Card> cards = new List<Card>();

            Suit[] suits = { Suit.Diamonds, Suit.Hearts, Suit.Clubs, Suit.Spades };
            CardRank[] ranks = { CardRank.Six, CardRank.Seven, CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King, CardRank.Ace };

            int cardsAdded = 0;
            while (cardsAdded < _numberOfCards)
            {
                foreach (var suit in suits)
                {
                    foreach (var rank in ranks)
                    {
                        if (cardsAdded >= _numberOfCards)
                            break;
                        cards.Add(new Card(suit, rank));
                        cardsAdded++;
                    }
                    if (cardsAdded >= _numberOfCards)
                        break;
                }
            }

            Shuffle(cards);
        }

        private void Shuffle(List<Card> cards)
        {
            Random random = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }

            foreach (var card in cards)
            {
                _deck.Enqueue(card);
            }
        }

        public void SetBet(long bet)
        {
            _bet = bet;
        }

        public override void PlayGame()
        {
            _playerCards.Clear();
            _computerCards.Clear();

            
            _playerCards.Add(_deck.Dequeue());
            _playerCards.Add(_deck.Dequeue());
            _computerCards.Add(_deck.Dequeue());
            _computerCards.Add(_deck.Dequeue());

            int playerScore = CalculateScore(_playerCards);
            int computerScore = CalculateScore(_computerCards);

            
            if (playerScore == 21 && computerScore == 21)
            {
                _resultMessage = "Both have Blackjack! It's a draw!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet));
                DisplayResult();
                return;
            }
            else if (playerScore == 21)
            {
                _resultMessage = "Player has Blackjack! Player wins!";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet * 2));
                DisplayResult();
                return;
            }
            else if (computerScore == 21)
            {
                _resultMessage = "Computer has Blackjack! Computer wins!";
                OnLooseInvoke(new GameResultEventArgs(_resultMessage, _bet, 0));
                DisplayResult();
                return;
            }

           
            while (playerScore < 21 && computerScore < 21 && playerScore == computerScore)
            {
                _playerCards.Add(_deck.Dequeue());
                _computerCards.Add(_deck.Dequeue());
                playerScore = CalculateScore(_playerCards);
                computerScore = CalculateScore(_computerCards);
            }

           
            if (playerScore > 21 && computerScore > 21)
            {
                _resultMessage = "Both busted! It's a draw!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet));
            }
            else if (playerScore > 21)
            {
                _resultMessage = $"Player busted! (Score: {playerScore}) Computer wins!";
                OnLooseInvoke(new GameResultEventArgs(_resultMessage, _bet, 0));
            }
            else if (computerScore > 21)
            {
                _resultMessage = $"Computer busted! (Score: {computerScore}) Player wins!";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet * 2));
            }
            else if (playerScore == computerScore)
            {
                _resultMessage = $"It's a draw! Both have {playerScore} points!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet));
            }
            else if (playerScore > computerScore)
            {
                _resultMessage = $"Player wins! {playerScore} vs {computerScore}";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, _bet * 2));
            }
            else
            {
                _resultMessage = $"Computer wins! {computerScore} vs {playerScore}";
                OnLooseInvoke(new GameResultEventArgs(_resultMessage, _bet, 0));
            }

            DisplayResult();
        }

        private int CalculateScore(List<Card> cards)
        {
            int score = 0;
            int aces = 0;

            foreach (var card in cards)
            {
                int value = card.GetValue();
                if (value == 11)
                {
                    aces++;
                }
                score += value;
            }

            
            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }

            return score;
        }

        public override void DisplayResult()
        {
            Console.WriteLine("\n===== BLACKJACK RESULT =====");
            Console.WriteLine($"Player's hand: {string.Join(", ", _playerCards)}");
            Console.WriteLine($"Player's score: {CalculateScore(_playerCards)}");
            Console.WriteLine($"Computer's hand: {string.Join(", ", _computerCards)}");
            Console.WriteLine($"Computer's score: {CalculateScore(_computerCards)}");
            Console.WriteLine($"Result: {_resultMessage}");
            Console.WriteLine("============================\n");
        }
    }
}
