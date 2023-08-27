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
        private static  InputValidation validator = new();
        private static readonly ConfigSettings settings = new();
        private static  WordGenerator? wordGenerator;
        private static DatabaseManager? databaseManager;
        private static GameStateHandler stateHandler = new();


        public int MaxGuesses => settings.MaxGuesses;
        public int CurrentGuesses => stateHandler.CurrentGuesses;
        public char[] CorrectlyGuessedLetters => stateHandler.CorrectlyGuessedLetters;
        public string GameStatus => stateHandler.GameStatus;

        public string Word =>  (wordGenerator != null) ? wordGenerator.Word : "No Word Generated";

        /// <summary>
        /// Main backend object to use to interact with front end elements
        /// </summary>
        public Backend(string host, string username, string password, string database) 
        { 
            // Field setup
            databaseManager = new(host, username, password, database);
            wordGenerator = new(databaseManager.Connection);

            // Difficulty importing from database
            int[] difficulties = FetchDifficultyBounds();
            validator = new(difficulties[0], difficulties[1]);

            // Game Setup
            wordGenerator.GenerateWord(settings.Difficulty);
            stateHandler = new(settings, Word);
        }


        public static void Input<T>(T input)
        {
            stateHandler.Input(input);
        }

        public static void SetNewWord()
        {
            if (validator.ValidateDifficulty(settings.Difficulty))
            {
                wordGenerator?.GenerateWord(settings.Difficulty);
                stateHandler.SetNewWord(settings, wordGenerator.Word);
            }
            else
            {
                throw new IndexOutOfRangeException(
                    $"Difficulty is out of bounds.Must be between {validator.difficultyBounds[0]} and {validator.difficultyBounds[1]}");
            }
        }

        public static void SetNewDifficulty(int newDifficulty)
        {
            if (validator.ValidateDifficulty(newDifficulty))
            {
                settings.UpdateDifficulty(newDifficulty);
                SetNewWord();
            }
        }

        public static void CustomizeGuessAmount(int guesses)
        {
            settings.UpdateGuessAmount(guesses);
        }

        private static int[] FetchDifficultyBounds()
        {
            int[] output = new int[2];

            // Reads the lowest difficulty setting 
            output[0] = databaseManager
                .GetIntegerFromDatabase("SELECT difficulty_id " +
                                          "FROM difficulty " +
                                          "LIMIT 1;");

            output[1] = databaseManager
                .GetIntegerFromDatabase("SELECT difficulty_id " +
                                          "FROM difficulty " +
                                          "ORDER BY difficulty_id DESC " +
                                          "LIMIT 1;");

            return output;
        }
    }
}
