using Final.core.enums;
using Final.Core.Enums;
using Final.Core.Models;
using Final.Core.Services;

namespace Final.Core.Games
{
    public class BlackjackGame : CasinoGameBase
    {
        private readonly int _numberOfCards;
        private Queue<Card> _deck = null!;
        private List<Card> _playerCards = null!;
        private List<Card> _computerCards = null!;
        private long _bet;
        private string _resultMessage = string.Empty;
        private int _playerScore;
        private int _computerScore;

        public BlackjackGame(int numberOfCards)
        {
            if (numberOfCards < 10 || numberOfCards > 52)
            {
                throw new ArgumentException("Number of cards must be between 10 and 52");
            }

            _numberOfCards = numberOfCards;
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            _playerCards = new List<Card>();
            _computerCards = new List<Card>();
            _deck = new Queue<Card>();

            var cards = new List<Card>();
            var suits = new[] { Suit.Diamonds, Suit.Hearts, Suit.Clubs, Suit.Spades };
            var ranks = new[] { CardRank.Six, CardRank.Seven, CardRank.Eight, CardRank.Nine,
                               CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King, CardRank.Ace };

            int cardsAdded = 0;
            while (cardsAdded < _numberOfCards)
            {
                foreach (var suit in suits)
                {
                    foreach (var rank in ranks)
                    {
                        if (cardsAdded >= _numberOfCards) break;
                        cards.Add(new Card(suit, rank));
                        cardsAdded++;
                    }
                    if (cardsAdded >= _numberOfCards) break;
                }
            }

            Shuffle(cards);
        }

        private void Shuffle(List<Card> cards)
        {
            var random = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            foreach (var card in cards)
            {
                _deck.Enqueue(card);
            }
        }

        public override void SetBet(long bet)
        {
            _bet = bet;
        }

        private void EnsureDeckHasCards(int needed)
        {
            if (_deck.Count < needed)
            {
                Console.WriteLine("🔄 Reshuffling deck...");
                InitializeDeck();
            }
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

        public override void PlayGame()
        {
            _playerCards.Clear();
            _computerCards.Clear();

            InitializeDeck();
            EnsureDeckHasCards(4);

            _playerCards.Add(_deck.Dequeue());
            _playerCards.Add(_deck.Dequeue());
            _computerCards.Add(_deck.Dequeue());
            _computerCards.Add(_deck.Dequeue());

            _playerScore = CalculateScore(_playerCards);
            _computerScore = CalculateScore(_computerCards);

            if (_playerScore == 21 && _computerScore == 21)
            {
                _resultMessage = "Both have Blackjack! It's a draw!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Draw));
                DisplayResult();
                return;
            }
            else if (_playerScore == 21)
            {
                _resultMessage = "🎉 Player has Blackjack! Player wins!";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Win));
                DisplayResult();
                return;
            }
            else if (_computerScore == 21)
            {
                _resultMessage = "💻 Computer has Blackjack! Computer wins!";
                OnLoseInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Lose));
                DisplayResult();
                return;
            }

            while (_playerScore < 17 && _computerScore < 21)
            {
                EnsureDeckHasCards(1);
                _playerCards.Add(_deck.Dequeue());
                _playerScore = CalculateScore(_playerCards);
            }

            while (_computerScore < 17 && _playerScore <= 21)
            {
                EnsureDeckHasCards(1);
                _computerCards.Add(_deck.Dequeue());
                _computerScore = CalculateScore(_computerCards);
            }

            DetermineWinner();
            DisplayResult();
        }

        private void DetermineWinner()
        {
            if (_playerScore > 21 && _computerScore > 21)
            {
                _resultMessage = "Both busted! It's a draw!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Draw));
            }
            else if (_playerScore > 21)
            {
                _resultMessage = $"💻 Player busted! (Score: {_playerScore}) Computer wins!";
                OnLoseInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Lose));
            }
            else if (_computerScore > 21)
            {
                _resultMessage = $"🎉 Computer busted! (Score: {_computerScore}) Player wins!";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Win));
            }
            else if (_playerScore == _computerScore)
            {
                _resultMessage = $"It's a draw! Both have {_playerScore} points!";
                OnDrawInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Draw));
            }
            else if (_playerScore > _computerScore)
            {
                _resultMessage = $"🎉 Player wins! {_playerScore} vs {_computerScore}";
                OnWinInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Win));
            }
            else
            {
                _resultMessage = $"💻 Computer wins! {_computerScore} vs {_playerScore}";
                OnLoseInvoke(new GameResultEventArgs(_resultMessage, _bet, GameResult.Lose));
            }
        }

        public override void DisplayResult()
        {
            Console.WriteLine("===== BLACKJACK RESULT =====");
            Console.WriteLine($"Player's hand: {string.Join(", ", _playerCards)}");
            Console.WriteLine($"Player's score: {_playerScore}");
            Console.WriteLine($"Computer's hand: {string.Join(", ", _computerCards)}");
            Console.WriteLine($"Computer's score: {_computerScore}");
            Console.WriteLine($"Result: {_resultMessage}");
            Console.WriteLine("============================");
        }
    }
}