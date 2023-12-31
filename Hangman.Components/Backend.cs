﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Components
{

    public class Backend
    {
        private readonly InputValidation validator = new();
        private readonly ConfigSettings settings = new();
        public ConfigSettings Settings => settings;
        private WordGenerator? wordGenerator;
        private DatabaseManager? databaseManager;
        private readonly GameStateHandler stateHandler = new();


        public int MaxGuesses => settings.MaxGuesses;
        public int IncorrectGuessAmount => stateHandler.IncorrectGuessAmount;
        public char[] CorrectlyGuessedLetters { get { return stateHandler.CorrectlyGuessedLetters; } }

        public List<char> IncorrectLetters { get { return stateHandler.IncorrectLetters; } }
        public List<string> IncorrectWords { get { return stateHandler.IncorrectWords; } }
        public string GameStatus { get { return stateHandler.GameStatus; } }

        public string Word => (wordGenerator != null) ? wordGenerator.Word : "No Word Generated";
        public int Difficulty => settings.Difficulty;

        public int Attempts { get { return stateHandler.Attempts; } }
        /// <summary>
        /// Main backend object to use to interact with front end elements
        /// </summary>
        public Backend(string host, string username, string password, string database)
        {
            // Field setup
            databaseManager = new(host, username, password, database);
            wordGenerator = new(databaseManager.Connection);

            // Difficulty importing from database
            int[] difficulties = FetchDifficultyBounds().GetAwaiter().GetResult();
            validator = new(difficulties[0], difficulties[1]);

            // Game Setup
            wordGenerator.GenerateWord(settings);
            stateHandler = new(settings, Word);
        }


        public void Input<T>(T inputValue)
        {
            string output = stateHandler.Input(inputValue);
            if (output != "")
            {
                Console.WriteLine(output);
            }
        }

        public void SetNewWord()
        {
            if (validator.ValidateDifficulty(settings.Difficulty))
            {
                wordGenerator?.GenerateWord(settings);
                stateHandler.SetNewWord(settings, wordGenerator?.Word
                    ?? throw new Exception("Word is null and therefore cannot have its value set to the current word"));
            }
            else
            {
                throw new IndexOutOfRangeException(
                    $"Difficulty is out of bounds.Must be between {validator.DifficultyBounds[0]} and {validator.DifficultyBounds[1]} Value: {settings.Difficulty}");
            }
        }

        public  void SetNewDifficulty(int newDifficulty)
        {
            if (validator.ValidateDifficulty(newDifficulty))
            {
                settings.UpdateDifficulty(newDifficulty);
                SetNewWord();
            }
        }

        public void CustomizeGuessAmount(int guesses) => settings.UpdateGuessAmount(guesses);

        private async Task<int[]> FetchDifficultyBounds()
        {
            int[] output = new int[2];

            if (databaseManager == null)
            {
                throw new NullReferenceException("DatabaseManager is null");
            }

            // Reads the lowest difficulty setting
            output[0] = await databaseManager
                .GetIntegerFromDatabase("SELECT difficulty_id " +
                                          "FROM difficulty " +
                                          "LIMIT 1;");

            output[1] = await databaseManager
                .GetIntegerFromDatabase("SELECT difficulty_id " +
                                          "FROM difficulty " +
                                          "ORDER BY difficulty_id DESC " +
                                          "LIMIT 1;");

            return output;
        }
    }
}
