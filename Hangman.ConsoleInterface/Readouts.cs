

using Hangman.Components;

namespace Hangman.ConsoleInterface
{
    internal class Readouts
    {
        private static readonly string lineBreak = "==================================================================\n";
        internal static void GameReadout(Backend backend)
        {
            Console.WriteLine(lineBreak);
            Console.WriteLine($"Current Difficulty: {backend.Difficulty}");
            Console.WriteLine($"Current Guesses: {backend.IncorrectGuessAmount}/{backend.MaxGuesses}");
            Console.WriteLine($"Guessed Words: {HelperFunctions.ConvertStringListToString(backend.IncorrectWords)}");
            Console.WriteLine($"Guessed Letters: {HelperFunctions.ConvertCharListToString(backend.IncorrectLetters)}");
            Console.WriteLine($"Correctly Guessed Letters: {HelperFunctions.ConvertCharListToString(backend.CorrectlyGuessedLetters.ToList())}\n");
            Console.WriteLine(lineBreak);

        }
        internal static void GameOverReadout(Backend backend)
        {
            Console.Clear();
            Console.WriteLine(lineBreak);
            Console.WriteLine($"Current Guesses: {backend.IncorrectGuessAmount}/{backend.MaxGuesses}");
            Console.WriteLine($"Correctly Guessed Letters: {HelperFunctions.ConvertCharListToString(backend.CorrectlyGuessedLetters.ToList())}\n");

            if (backend.GameStatus == "Game Won")
            {
                Console.WriteLine("You Won!");
            }
            else
            {
                Console.WriteLine("You Lost!");
                Console.WriteLine($"Correct word was {backend.Word}");
            }
            int correctGuesses = backend.Attempts - backend.IncorrectGuessAmount;
            Console.WriteLine($"Total guesses: {backend.Attempts} of which {correctGuesses} were right.");
        }

        internal static void MainMenuReadout()
        {
            Console.Clear();
            Console.WriteLine($"{lineBreak}" +
                              $"1. Play Game\n" +
                              $"2. Change Difficulty\n" +
                              $"3. Quit Game\n" +
                              $"{lineBreak}");

            Console.Write("Enter an option: ");

        }

        internal static void ChangeDifficultyReadout(Backend backend)
        {
            ConfigSettings config = backend.settings;

            Console.Clear();
            Console.WriteLine($"Current Difficulty: {config.Difficulty}");
            Console.WriteLine($"{lineBreak}" +
                              $"1. Easy\n" +
                              $"2. Medium\n" +
                              $"3. Hard\n" +
                              $"4. Very Hard\n" +
                              $"{lineBreak}");


            Console.Write("Enter an option: ");
        }

    }
}
