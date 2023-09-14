using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Components
{
    internal class InputHandler
    {
        // TODO: Migrate functionality for InputHandler from InputValidation
        private GameStateHandler gameStateHandler;
        public InputHandler(GameStateHandler handler) => gameStateHandler = handler;
        internal string HandleInput<T>(T input)
        {
            string output = "";
            if (typeof(T) == typeof(string))
            {
                string? cleanInput = input?.ToString();
                try
                {
                    HandleString(cleanInput);
                }
                catch (InvalidOperationException ex)
                {
                    output = "Word has already been guessed";
                }
            }
            else if (typeof(T) == typeof(char))
            {
                string? stringInput = input?.ToString();

                if (stringInput == null)
                {
                    throw new NullReferenceException($"Character input is null and therefore cannot be processed, Value {stringInput}");
                }
                char cleanInput = stringInput[0];

                try
                {
                    HandleChar(cleanInput);
                }
                catch
                {
                    output = "Letter has already been guessed";
                }
            }
            else
            {
                throw new InvalidDataException("Invalid type for input");
            }

            // Check to see if the game is over
            gameStateHandler.MaxGuessChecks();
            return output;

        }

        private void HandleString(string? input)
        {
            // Guard Statement to prevent null value exceptions
            if (input == null)
            {
                throw new NullReferenceException($"String input is null and therefore cannot be processed, Value {input}");
            }

            if (RepeatedWord(input, gameStateHandler.IncorrectWords))
            {
                throw new InvalidOperationException(input + " has already been guessed. Please configure front end validation to prevent these values.");
            }

            if (!InputValidation.ValidateInput(input))
            {
                throw new InvalidOperationException(input + " is not a valid input. Please configure front end validation to prevent these values.");
            }

            // Handles a win by correct guess
            if (gameStateHandler.IsCorrectGuessString(input))
            {
                gameStateHandler.SetWin();
            }
            else
            {
                gameStateHandler.IncorrectWordGuess(input);
            }

        }

        private void HandleChar(char input)
        {
            InputValidation validator = new();
            if (!validator.ValidateInput(input))
            {
                throw new InvalidOperationException(input + " is not a valid input. Please configure front end validation to prevent these values.");
            }

            // Check to see if the character has already been guessed
            if (RepeatedLetter(input, gameStateHandler.CorrectlyGuessedLetters, gameStateHandler.IncorrectLetters))
            {
                throw new InvalidOperationException(input + " has already been guessed. Please configure front end validation to prevent these values.");
            }

            if (gameStateHandler.IsCorrectGuessChar(input))
            {

               gameStateHandler.CorrectGuessChar(input);
            }
            else
            {
                gameStateHandler.IncorrectGuessChar(input);
            }
        }

        private bool RepeatedLetter(char letter, char[] correctlyGuessedLetters, List<char> incorrectLetters) => correctlyGuessedLetters.Contains(letter) || incorrectLetters.Contains(letter);

        private bool RepeatedWord(string word, List<string> incorrectWords) => incorrectWords.Contains(word);
    }
}
