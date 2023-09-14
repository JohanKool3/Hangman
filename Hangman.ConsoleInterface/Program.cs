using Microsoft.Extensions.Configuration;
using Hangman.Components;

namespace Hangman
{
    public class Program
    {

        private static Backend backend = SetupBackend();

        private static Backend SetupBackend()
        {
            // Fetch the information from the user secrets file
            var config = new ConfigurationBuilder()
                        .AddUserSecrets<Program>()
                        .Build();

            // Setup Database Connection (defaults are examples)
            string host = config["Host"] ?? "localhost";
            string username = config["Username"] ?? "username";
            string password = config["Password"] ?? "password";
            string database = config["Database"] ?? "database";

            return new Backend(host, username, password, database);
        }

        private static string ConvertCharListToString(List<char> charList)
        {
            string output = "";
            foreach(char character in charList)
            {
                if(character != '\0')
                {
                    output += $"{character} ";
                }
                else
                {
                    output += "_ ";
                }
            }

            return output;
        }

        private static string ConvertStringListToString(List<string> strList)
        {
            string output = "";
            foreach (string str in strList)
            {
                output += $"{str}, ";
            }

            if (output.Length > 2)
            {
                return output[..^2];
            }
            else
            {
                return output;
            }
        }

        private static void GameReadout()
        {
            Console.WriteLine($"Current Guesses: {backend.CurrentGuesses}/{backend.MaxGuesses}");
            Console.WriteLine($"Guessed Words: {ConvertStringListToString(backend.IncorrectWords)}");
            Console.WriteLine($"Guessed Letters: {ConvertCharListToString(backend.IncorrectLetters)}");
            Console.WriteLine($"Correctly Guessed Letters: {ConvertCharListToString(backend.CorrectlyGuessedLetters.ToList())}\n");

        }
        private static void GameOverReadout()
        {
            Console.Clear();
            Console.WriteLine($"Current Guesses: {backend.CurrentGuesses}/{backend.MaxGuesses}");
            Console.WriteLine($"Correctly Guessed Letters: {ConvertCharListToString(backend.CorrectlyGuessedLetters.ToList())}\n");

            if(backend.GameStatus == "Game Won")
            {
                Console.WriteLine("You Won!");
            }
            else
            {
                Console.WriteLine("You Lost!");
                Console.WriteLine($"Correct word was {backend.Word}");
            }
        }

        private static void TakeInput()
        {
            Console.Write("Enter a word or letter: ");
            string? input = Console.ReadLine();
            Console.Clear();

            if (input == null || !ValidateInput(input)) // Invalid Value inputs
            {
                Console.WriteLine("Must enter a valid value");


            }
            else if (input.Length == 1)
            {
                backend.Input(input[0]);

            }
            else
            {
                backend.Input(input);
            }

        }

        private static bool ContinueGame()
        {
            bool validOutput = false;
            while (!validOutput) 
            { 
                Console.WriteLine("Would you like to play again? Y/N");
                string? output = Console.ReadLine();
                char response =  (output == null) ?' ' :output.ToLower()[0];

                if (response == 'y')
                {
                    backend.SetNewWord();
                    return true;
                }
                else if(response == 'n')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("You must enter either Y or N");
                }
                Console.Clear();
            }
            return false;

        }

        private static bool ValidateInput(string input)
        {
            List<char> characters = input.ToLower().ToList();

            foreach (char character in characters)
            {
                if (!char.IsLetter(character))
                {
                    return false;
                }
            }
            return true;
        }

        public static void Main()
        {
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();
                while (backend.GameStatus == "Running")
                {
                    GameReadout();
                    TakeInput();
                }
                GameOverReadout();

                gameRunning = ContinueGame();
            }
            Console.WriteLine("Thank you for playing");
        }
    }
}