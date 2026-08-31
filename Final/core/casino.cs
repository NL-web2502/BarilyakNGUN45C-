using Final.core.models;
using Final.Core.Games;
using Final.Core.Interfaces;
using Final.Core.Models;
using Final.Core.Services;
using System.Text.Json;

namespace Final.Core
{
    public class Casino : IGame
    {
        private const string SavePath = "Profiles";
        private const long MaxBank = 1_000_000_000;
        private const long MinBet = 10;

        private PlayerProfile _playerProfile = null!;
        private ISaveLoadService<string> _saveLoadService = null!;
        private CasinoGameBase? _currentGame;
        private bool _isGameActive;
        private bool _halfBankWasted;

        public Casino()
        {
            _saveLoadService = new FileSystemSaveLoadService(SavePath);
            _isGameActive = true;
            _halfBankWasted = false;
        }

        public void StartGame()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("WELCOME TO CASINO!");
            Console.WriteLine("========================================\n");

            LoadOrCreateProfile();

            while (_isGameActive)
            {
                DisplayMenu();

                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (input == "0")
                {
                    _isGameActive = false;
                    Console.WriteLine("\nThank you playing! Goodbye!\n");
                    SaveProfile();
                    break;
                }

                if (input == "1" || input == "2")
                {
                    ProcessGameSelection(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please select 1, 2, or 0.\n");
                }
            }
        }

        private void LoadOrCreateProfile()
        {
            try
            {
                string profileData = _saveLoadService.LoadData("profile");

                if (!string.IsNullOrEmpty(profileData))
                {
                    _playerProfile = JsonSerializer.Deserialize<PlayerProfile>(profileData);
                   
                if (_playerProfile != null)
                    {
                        Console.WriteLine($"Welcome back, {_playerProfile.Name}!");
                        Console.WriteLine($"Your current bank: ${_playerProfile.Bank}\n");
                        return;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to load profile. Creating a new one...\n");
            }

            Console.Write("Enter your name: ");
            string name = Console.ReadLine()?.Trim() ?? "Player";
            if (string.IsNullOrEmpty(name))
                name = "Player";

            _playerProfile = new PlayerProfile(name);
            Console.WriteLine($"Welcome, {_playerProfile.Name}! Your starting bank is ${_playerProfile.Bank}\n");
            SaveProfile();
        }

        private void DisplayMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"{_playerProfile.Name}'s Casino");
            Console.WriteLine($"Your Bank: ${_playerProfile.Bank}");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Blackjack");
            Console.WriteLine("2. Dice Game");
            Console.WriteLine("0. Exit and Save");
            Console.WriteLine("========================================");
            Console.Write("Select an option: ");
        }

        private void ProcessGameSelection(string input)
        {
            if (_playerProfile.Bank < MinBet)
            {
                Console.WriteLine($"\nYou don't have enough money to play (minimum bet: ${MinBet}). Kicked!\n");
                _isGameActive = false;
                SaveProfile();
                return;
            }

            Console.Write($"Enter your bet (min: ${MinBet}, max: ${_playerProfile.Bank}): $");
            string betInput = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!long.TryParse(betInput, out long bet))
            {
                Console.WriteLine("Invalid bet amount.\n");
                return;
            }

            if (bet < MinBet)
            {
                Console.WriteLine($"Minimum bet is ${MinBet}\n");
                return;
            }

            if (bet > _playerProfile.Bank)
            {
                Console.WriteLine($"You don't have enough money. Your bank: ${_playerProfile.Bank}\n");
                return;
            }

            _currentGame = input == "1"
                ? new BlackjackGame(20)
                : new DiceGame(3, 1, 6);

            _currentGame.OnWin += HandleWin;
            _currentGame.OnLose += HandleLose;
            _currentGame.OnDraw += HandleDraw;

            _currentGame.SetBet(bet);
            _currentGame.PlayGame();

            if (_currentGame != null)
            {
                _currentGame.OnWin -= HandleWin;
                _currentGame.OnLose -= HandleLose;
                _currentGame.OnDraw -= HandleDraw;
            }

            _currentGame = null;
            SaveProfile();
        }

        private void HandleWin(object? sender, GameResultEventArgs e)
        {
            long winAmount = e.BetAmount * 2;
            _playerProfile.Bank += winAmount;
            Console.WriteLine($"\n{e.Message}");
            Console.WriteLine($"💰 You won ${winAmount - e.BetAmount} profit!");
            CheckBankLimits();
        }

        private void HandleLose(object? sender, GameResultEventArgs e)
        {
            _playerProfile.Bank -= e.BetAmount;
            Console.WriteLine($"\n{e.Message}");
            Console.WriteLine($"💸 You lost ${e.BetAmount}!");
            CheckBankLimits();
        }

        private void HandleDraw(object? sender, GameResultEventArgs e)
        {
            Console.WriteLine($"\n{e.Message}");
            Console.WriteLine($"🤝 Your bet is returned!");
            CheckBankLimits();
        }

        private void CheckBankLimits()
        {
            if (_playerProfile.Bank > MaxBank)
            {
                long excess = _playerProfile.Bank - MaxBank;
                _playerProfile.Bank = MaxBank;
                Console.WriteLine($"\n🎉 **Congratulations! You broke the casino!**");
                Console.WriteLine($"Excess: ${excess}");
                Console.WriteLine($"Your bank reset to: ${_playerProfile.Bank}\n");
                _halfBankWasted = false;
            }

            if (_playerProfile.Bank > MaxBank / 2 && !_halfBankWasted && _playerProfile.Bank <= MaxBank)
            {
                _halfBankWasted = true;
                _playerProfile.Bank /= 2;
                Console.WriteLine("\n🍺 **You wasted half of your bank money in casino's bar**");
                Console.WriteLine($"Your bank is now: ${_playerProfile.Bank}\n");
            }

            Console.WriteLine($"Current bank: ${_playerProfile.Bank}\n");
        }

        private void SaveProfile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_playerProfile);
                _saveLoadService.SaveData(json, "profile");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving profile: {ex.Message}");
            }
        }
    }
}