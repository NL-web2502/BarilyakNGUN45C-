using GamePrototype.Game;
using GamePrototype.Utils;

namespace GamePrototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome, player!");
            Console.WriteLine("Select difficulty (Easy/Hard):");

            string input = Console.ReadLine();
            Difficulty difficulty = Difficulty.Easy;

            if (Enum.TryParse<Difficulty>(input, true, out var parsedDifficulty))
            {
                difficulty = parsedDifficulty;
            }
            else
            {
                Console.WriteLine("Invalid difficulty. Setting to Easy.");
            }

            new GameLoop(difficulty).StartGame();
        }
    }
}