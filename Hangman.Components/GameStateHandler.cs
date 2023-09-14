
namespace Hangman.Components
{
    internal class GameStateHandler
    {

        private int currentGuesses;
        public int CurrentGuesses { get { return currentGuesses; } }
        private int maxGuesses;
        public int MaxGuesses { get { return maxGuesses; } }

        private char[] correctlyGuessedLetters = new char[1]; // Default value to keep the compiler happy
        public char[] CorrectlyGuessedLetters { get { return correctlyGuessedLetters; } }
        private char[] wordLetters = new char[1];
        private string word = "";

        private readonly List<char> incorrectLetters = new();
        public List<char> IncorrectLetters { get { return incorrectLetters; } }


        private List<string> incorrectWords = new();
        public List<string> IncorrectWords { get { return incorrectWords; } }

        private bool gameWon;

        private bool complete;
        public string GameStatus { get => complete ? GenerateEndGameStatus() : "Running"; }

        // Setup the word

        internal GameStateHandler():this(new ConfigSettings(), "default") { }

        internal GameStateHandler(string wordIn) : this(new ConfigSettings(), wordIn) { }

        internal GameStateHandler(ConfigSettings settings, string wordIn)
        {
            ConfigureNewState(settings, wordIn);
        }


        public string Input<T>(T input)
        {
            if (!complete)
            {
                return HandleInput(input);
            }
            return "";
        }

        public void SetNewWord(ConfigSettings settings, string wordIn) => ConfigureNewState(settings, wordIn.ToLower());


        private void ConfigureNewState(ConfigSettings settings, string wordIn)
        {
            currentGuesses = 0;
            maxGuesses = settings.MaxGuesses;
            ConfigureWordFields(wordIn);
            word = wordIn.ToLower();
        }

        private string HandleInput<T>(T input)
        {
            string output = "";
            if(typeof(T) == typeof(string))
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
            else if(typeof(T) == typeof(char))
            {
                string? stringInput = input?.ToString();

                if(stringInput == null)
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

        #region Input Handlers
        private void HandleString(string? input)
        {
            // Guard Statement to prevent null value exceptions
            if (input == null)
            {
                throw new NullReferenceException($"String input is null and therefore cannot be processed, Value {input}");
            }

            if(RepeatedWord(input))
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

        #endregion

        internal bool IsCorrectGuessString(string input)
            => (input == word);
        internal bool IsCorrectGuessChar(char input)
            => wordLetters.Any(character => (character == input));

        internal void MaxGuessChecks()
        {
            if(currentGuesses >= maxGuesses)
            {
                complete = true;
            }
        }
        private void ConfigureWordFields(string word)
        {
            int wordLength = word.Length;

            correctlyGuessedLetters = new char[wordLength];
            wordLetters = word.Select(letter =>  letter).ToArray();
            ConfigureNonLetterCharacters();
        }

        private List<int> GetIndexOfLetter(char letter)
        {
            List<int> output = new();

            for(int i= 0; i < wordLetters.Length; i++)
            {
                if (wordLetters[i] == letter)
                {
                    output.Add(i);
                }
            }
            return output;
        }

        private void ConfigureNonLetterCharacters()
        {
            foreach(int index in GetIndexOfLetter(' '))
            {
                correctlyGuessedLetters[index] = ' ';
            }

            foreach(int index in GetIndexOfLetter('-'))
            {
                correctlyGuessedLetters[index] = '-';
            }
        }

        private bool RepeatedLetter(char letter) => correctlyGuessedLetters.Contains(letter) || incorrectLetters.Contains(letter);

        private bool RepeatedWord(string word) => incorrectWords.Contains(word);

        private string GenerateEndGameStatus()
            => (gameWon) ? "Game Won" : "Game Lost";


    }
}
