using Microsoft.Extensions.Configuration;
using Hangman.Components;

namespace Hangman
{
    public class Program
    {

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
                return output.Substring(0, output.Length - 2);
            }
            else
            {
                return output;
            }
        }

        private static void GameReadout(Backend backend)
        {
            Console.Clear();
            Console.WriteLine($"Current Guesses: {backend.CurrentGuesses}/{backend.MaxGuesses}");
            Console.WriteLine($"Guessed Words: {ConvertStringListToString(backend.IncorrectWords)}");
            Console.WriteLine($"Guessed Letters: {ConvertCharListToString(backend.IncorrectLetters)}");
            Console.WriteLine($"Correctly Guessed Letters: {ConvertCharListToString(backend.CorrectlyGuessedLetters.ToList())}\n");

        }
        private static void GameOverReadout(Backend backend)
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

        private static void TakeInput(Backend backend)
        {
            Console.WriteLine("Enter a word or letter:");
            string? input = Console.ReadLine();

            if(input == null)
            {
                Console.WriteLine("Must enter a valid value");
            }
            else if(input.Length == 1)
            {
                backend.Input(input[0]);
            }
            else
            {
                backend.Input(input);
            }

        }

        public static void Main()
        {
            Backend backend = SetupBackend();


            while(backend.GameStatus == "Running")
            {
                GameReadout(backend);
                TakeInput(backend);
            }
            GameOverReadout(backend);

        }
    }
}