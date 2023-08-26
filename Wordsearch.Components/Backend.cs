using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch.Components
{
    
    public class Backend
    {
        private static readonly InputValidation validator = new();
        private static readonly ConfigSettings settings = new();
        private static  WordGenerator wordGenerator;

        private NpgsqlConnection? _connection;


        public string Word => wordGenerator.Word;

        /// <summary>
        /// Main backend object to use to interact with front end elements
        /// </summary>
        public Backend(string host, string username, string password, string database) 
        { 
            SetupDatabaseConnection(host, username, password, database);
            wordGenerator = new(_connection);

            // Generates a word by default
            wordGenerator.GenerateWord(settings.Difficulty);
        }

        public static void SetNewWord()
        {
            if (validator.ValidateDifficulty(settings.Difficulty))
            {
                wordGenerator.GenerateWord(settings.Difficulty);
            }
            else
            {
                // TODO: Handle invalid inputs
            }
        }

        public static void SetNewDifficulty(int newDifficulty)
        {
            if (validator.ValidateDifficulty(newDifficulty))
            {
                settings.UpdateDifficulty(newDifficulty);
            }
        }

        public static void CustomizeGuessAmount(int guesses)
        {
            settings.UpdateGuessAmount(guesses);
        }

        internal void SetupDatabaseConnection(string host, string username, string password, string database)
        {
            string connString = $"Host={host};Username={username};Password={password};Database={database}";
            _connection = new NpgsqlConnection(connString);
            _connection.Open();
        }
    }
}
