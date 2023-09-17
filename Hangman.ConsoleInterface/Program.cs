using Microsoft.Extensions.Configuration;
using Hangman.Components;
using Hangman.ConsoleInterface;
using System.Data;

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

        private static bool ContinueGame()
        {
            bool validOutput = false;
            while (!validOutput)
            {
                Console.WriteLine("Would you like to play again? Y/N");
                string? output = Console.ReadLine();
                char response =  (output == null) ?' ' :output.ToLowerInvariant()[0];

                if (response == 'y')
                {
                    backend.SetNewWord();
                    Console.Clear();
                    return true;
                }
                else if(response == 'n')
                {
                    Console.Clear();
                    return false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You must enter either Y or N");
                }

            }
            return false;

        }

        private static void RunGame()
        {
            Console.Clear();
            bool gameRunning = true;
            _ = "";
            // Database connection check

            while (gameRunning)
            {
                Console.Clear();
                while (backend.GameStatus == "Running")
                {
                    Readouts.GameReadout(backend);
                    UserInput.TakeGuess(backend);
                }
                Readouts.GameOverReadout(backend);

                gameRunning = ContinueGame();
            }
        }

        private static void ChangeDifficulty()
        {
            Readouts.ChangeDifficultyReadout(backend);
            int newDifficulty = UserInput.TakeNumberInput(1, 5, Console.ReadLine());
            if(newDifficulty == 5)
            {
                return;
            }
            backend.SetNewDifficulty(newDifficulty);
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void Main()
        {
            bool menuRunning = true;


            while (menuRunning) {

                try
                {
                    _ = backend.GameStatus;
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Error connecting to database, please make sure the database is running or user secrets has been configured correctly");
                    menuRunning = false;
                }
                // Display menu
                Readouts.MainMenuReadout(backend);

                // Take input for option
                string? userInput = Console.ReadLine();
                int userOption = UserInput.TakeNumberInput(1, 3, userInput);

                // if option is 1, start game
                switch (userOption)
                {
                    case 1:
                        RunGame();
                        break;
                    case 2:
                        ChangeDifficulty();
                        break;
                    case 3:
                        menuRunning = false;
                        break;
                    default:
                        DisplayErrorMessage($"Invalid Input, must be an option between {1} and {3}");
                        break;
                }
            }
            Console.WriteLine("Thank you for playing");
        }
    }
}