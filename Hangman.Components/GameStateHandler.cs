
namespace Wordsearch.Components
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

        private List<char> incorrectLetters = new();
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


        public void Input<T>(T input)
        {
            if (!complete)
            {
                HandleInput(input);
            }
        }

        public void SetNewWord(ConfigSettings settings, string wordIn) => ConfigureNewState(settings, wordIn);


        private void ConfigureNewState(ConfigSettings settings, string wordIn)
        {
            currentGuesses = 0;
            maxGuesses = settings.MaxGuesses;
            ConfigureWordFields(wordIn);
            word = wordIn;
        }

        private void HandleInput<T>(T input)
        {

            if(typeof(T) == typeof(string))
            {
                string? cleanInput = input?.ToString();

                // Guard Statement to prevent null value exceptions
                if(cleanInput == null)
                {
                    throw new NullReferenceException($"String input is null and therefore cannot be processed, Value {cleanInput}");
                }


                if (IsCorrectGuessString(cleanInput))
                {
                    complete = true;
                    gameWon = true;
                }
                else
                {
                    currentGuesses++;
                    incorrectWords.Add(cleanInput);
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

                if (IsCorrectGuessChar(cleanInput))
                {
                    correctlyGuessedLetters[GetIndexOfLetter(cleanInput)] = cleanInput;
                }
                else
                {
                    currentGuesses++;
                    incorrectLetters.Add(cleanInput);
                }
            }
            else
            {
                throw new InvalidDataException("Invalid type for input");
            }

            // Check to see if the game is over
            MaxGuessChecks();

        }

        private bool IsCorrectGuessString(string input)
            => (input == word);
        private bool IsCorrectGuessChar(char input)
            => wordLetters.Any(character => (character == input));

        private void MaxGuessChecks()
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
        }

        private int GetIndexOfLetter(char letter)
            => Array.IndexOf(wordLetters, letter);

        private string GenerateEndGameStatus()
            => (gameWon) ? "Game Won" : "Game Lost";


    }
}
