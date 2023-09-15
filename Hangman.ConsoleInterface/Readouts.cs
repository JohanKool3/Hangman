

using Hangman.Components;

namespace Hangman.ConsoleInterface
{
    internal class Readouts
    {
        private static readonly string lineBreak = "==================================================================";
        internal static void GameReadout(Backend backend)
        {
            Console.WriteLine($"Current Guesses: {backend.IncorrectGuessAmount}/{backend.MaxGuesses}");
            Console.WriteLine($"Guessed Words: {HelperFunctions.ConvertStringListToString(backend.IncorrectWords)}");
            Console.WriteLine($"Guessed Letters: {HelperFunctions.ConvertCharListToString(backend.IncorrectLetters)}");
            Console.WriteLine($"Correctly Guessed Letters: {HelperFunctions.ConvertCharListToString(backend.CorrectlyGuessedLetters.ToList())}\n");

        }
        internal static void GameOverReadout(Backend backend)
        {
            Console.Clear();
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
            Console.WriteLine($"{lineBreak}" +
                              $"1. Play Game" +
                              $"2. Change Difficulty" +
                              $"3. Quit Game" +
                              $"{lineBreak}");
        }

        internal static void ChangeDifficultyReadout(Backend backend)
        {
            ConfigSettings config = backend.settings;

            Console.WriteLine($"Current Difficulty: {config.Difficulty}");

            Console.WriteLine($"{lineBreak}" +
                              $"1. Easy" +
                              $"2. Medium" +
                              $"3. Hard" +
                              $"4. Custom" +
                              $"{lineBreak}");
        }

    }
}
