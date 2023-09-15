using Microsoft.Extensions.Configuration;
using Hangman.Components;
using Hangman.ConsoleInterface;

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

        public static void Main()
        {
            bool gameRunning = true;
            string runningStatus = "";

            // Database connection check
            try
            {
                runningStatus = backend.GameStatus;
            }
            catch(System.Exception)
            {
                Console.WriteLine("Error connecting to database, please make sure the database is running or user secrets has been configured correctly");
                gameRunning = false;
            }


            while (gameRunning)
            {
                Console.Clear();
                while (runningStatus == "Running")
                {
                    Readouts.GameReadout(backend);
                    UserInput.TakeGuess(backend);
                }
                Readouts.GameOverReadout(backend);

                gameRunning = ContinueGame();
            }
            Console.WriteLine("Thank you for playing");
        }
    }
}