using Final.core.games;
using Final.core.interfaces;
using Final.core.models;
using Final.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Final.core
{
    public class Casino : IGame
    {
        private const string SAVE_PATH = "Profiles";
        private const long MAX_BANK = 1_000_000_000;
        private const long MIN_BET = 10;

        private PlayerProfile _playerProfile;
        private ISaveLoadService<string> _saveLoadService;
        private BlackjackGame _blackjackGame;
        private DiceGame _diceGame;
        private bool _isGameActive;

        public Casino()
        {
            _saveLoadService = new FileSystemSaveLoadService(SAVE_PATH);
            _blackjackGame = new BlackjackGame(20);
            _diceGame = new DiceGame(3, 1, 6);
            _isGameActive = true;
        }

        public void StartGame()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   WELCOME TO CASINO!");
            Console.WriteLine("========================================\n");

            LoadOrCreateProfile();

            while (_isGameActive)
            {
                DisplayMenu();

                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (input == "0")
                {
                    _isGameActive = false;
                    Console.WriteLine("\nThank you for playing! Goodbye!\n");
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
            string profileData = _saveLoadService.LoadData("profile");

            if (!string.IsNullOrEmpty(profileData))
            {
                try
                {
                    _playerProfile = JsonSerializer.Deserialize<PlayerProfile>(profileData);
                    Console.WriteLine($"Welcome back, {_playerProfile.Name}!");
                    Console.WriteLine($"Your current bank: ${_playerProfile.Bank}\n");
                    return;
                }
                catch
                {
                    Console.WriteLine("Failed to load profile. Creating a new one...\n");
                }
            }

            Console.Write("Enter your name: ");
            string name = Console.ReadLine()?.Trim() ?? "Player";
            if (string.IsNullOrEmpty(name))
            {
                name = "Player";
            }

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
            if (_playerProfile.Bank <= 0)
            {
                Console.WriteLine("\nNo money? Kicked!\n");
                _isGameActive = false;
                SaveProfile();
                return;
            }

            Console.Write($"Enter your bet (min: ${MIN_BET}, max: ${_playerProfile.Bank}): $");
            string betInput = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!long.TryParse(betInput, out long bet))
            {
                Console.WriteLine("Invalid bet amount.\n");
                return;
            }

            if (bet < MIN_BET)
            {
                Console.WriteLine($"Minimum bet is ${MIN_BET}\n");
                return;
            }

            if (bet > _playerProfile.Bank)
            {
                Console.WriteLine($"You don't have enough money. Your bank: ${_playerProfile.Bank}\n");
                return;
            }

            if (input == "1")
            {
                PlayBlackjack(bet);
            }
            else if (input == "2")
            {
                PlayDiceGame(bet);
            }
        }

        private void PlayBlackjack(long bet)
        {
            _blackjackGame.SetBet(bet);

           
            _blackjackGame.OnWin += (sender, e) => HandleGameResult(e);
            _blackjackGame.OnLoose += (sender, e) => HandleGameResult(e);
            _blackjackGame.OnDraw += (sender, e) => HandleGameResult(e);

            _blackjackGame.PlayGame();

           
            _blackjackGame.OnWin -= (sender, e) => HandleGameResult(e);
            _blackjackGame.OnLoose -= (sender, e) => HandleGameResult(e);
            _blackjackGame.OnDraw -= (sender, e) => HandleGameResult(e);

            SaveProfile();
        }

        private void PlayDiceGame(long bet)
        {
            _diceGame.SetBet(bet);

           
            _diceGame.OnWin += (sender, e) => HandleGameResult(e);
            _diceGame.OnLoose += (sender, e) => HandleGameResult(e);
            _diceGame.OnDraw += (sender, e) => HandleGameResult(e);

            _diceGame.PlayGame();

           
            _diceGame.OnWin -= (sender, e) => HandleGameResult(e);
            _diceGame.OnLoose -= (sender, e) => HandleGameResult(e);
            _diceGame.OnDraw -= (sender, e) => HandleGameResult(e);

            SaveProfile();
        }

        private void HandleGameResult(GameResultEventArgs e)
        {
            _playerProfile.Bank += e.ResultAmount - e.BetAmount;

            if (_playerProfile.Bank > MAX_BANK)
            {
                long excess = _playerProfile.Bank - MAX_BANK;
                _playerProfile.Bank = MAX_BANK;
                Console.WriteLine($"\n**Congratulations! You broke the casino!**");
                Console.WriteLine($"Excess: ${excess}");
                Console.WriteLine($"Your bank reset to: ${_playerProfile.Bank}\n");
            }

            if (_playerProfile.Bank > MAX_BANK / 2)
            {
                _playerProfile.Bank /= 2;
                Console.WriteLine("\n**You wasted half of your bank money in casino's bar**");
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
                Console.WriteLine("Profile saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving profile: {ex.Message}");
            }
        }
    }
}
