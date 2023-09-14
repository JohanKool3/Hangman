
namespace Hangman.Components
{
    partial class GameStateHandler
    {

        public string Input<T>(T input)
        {
            if (!complete)
            {
                return HandleInput(input);
            }
            return "";
        }

        private string HandleInput<T>(T input)
        {
            string output = "";
            if (typeof(T) == typeof(string))
            {
                string? cleanInput = input?.ToString();
                try
                {
                    HandleString(cleanInput);
                }
                catch
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
            MaxGuessChecks();
            return output;

        }
        private void HandleString(string? input)
        {
            // Guard Statement to prevent null value exceptions
            if (input == null)
            {
                throw new NullReferenceException($"String input is null and therefore cannot be processed, Value {input}");
            }

            if (RepeatedWord(input))
            {
                throw new InvalidOperationException(input + " has already been guessed. Please configure front end validation to prevent these values.");
            }

            if (!InputValidation.ValidateInput(input))
            {
                throw new InvalidOperationException(input + " is not a valid input. Please configure front end validation to prevent these values.");
            }

            // Handles a win by correct guess
            if (IsCorrectGuessString(input))
            {
                complete = true;
                gameWon = true;
            }
            else
            {
                currentGuesses++;
                incorrectWords.Add(input);
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
            if (RepeatedLetter(input))
            {
                throw new InvalidOperationException(input + " has already been guessed. Please configure front end validation to prevent these values.");
            }

            if (IsCorrectGuessChar(input))
            {

                foreach (int index in GetIndexOfLetter(input))
                {
                    correctlyGuessedLetters[index] = input;
                }

                // Check to see if there are any empty letters, If not then the game is won
                if (!correctlyGuessedLetters.Any(letter => letter == '\0'))
                {
                    complete = true;
                    gameWon = true;
                }
            }
            else
            {
                currentGuesses++;
                incorrectLetters.Add(input);
            }
        }
    }
}
