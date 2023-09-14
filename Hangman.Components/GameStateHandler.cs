
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

        private List<char> incorrectLetters = new();
        public List<char> IncorrectLetters { get { return incorrectLetters; } }


        private List<string> incorrectWords = new();
        public List<string> IncorrectWords { get { return incorrectWords; } }

        private bool gameWon;

        private bool complete;
        public string GameStatus { get => complete ? GenerateEndGameStatus() : "Running"; }

        private static InputHandler? handler;

        // Setup the word

        internal GameStateHandler():this(new ConfigSettings(), "default") { }

        internal GameStateHandler(string wordIn) : this(new ConfigSettings(), wordIn) { }

        internal GameStateHandler(ConfigSettings settings, string wordIn)
        {
            ConfigureNewState(settings, wordIn);
            handler = new(this);
        }


        public string Input<T>(T input)
        {
            if (!complete)
            {
                return handler?.HandleInput(input) ?? throw new NullReferenceException("Handler must not be null");
            }
            return "";
        }

        public void SetNewWord(ConfigSettings settings, string wordIn) => ConfigureNewState(settings, wordIn.ToLower());


        private void ConfigureNewState(ConfigSettings settings, string wordIn)
        {
            gameWon = false;
            complete = false;
            currentGuesses = 0;
            maxGuesses = settings.MaxGuesses;
            ConfigureWordFields(wordIn);
            word = wordIn.ToLower();
        }

        internal void SetWin()
        {
            complete = true;
            gameWon = true;
        }

        internal void IncorrectWordGuess(string word)
        {
            currentGuesses++;
            incorrectWords.Add(word);
        }

        internal void CorrectGuessChar(char input)
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

        internal void IncorrectGuessChar(char input)
        {
            currentGuesses++;
            incorrectLetters.Add(input);
        }
        

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
            incorrectLetters = new();
            incorrectWords = new();

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

        private string GenerateEndGameStatus()
            => (gameWon) ? "Game Won" : "Game Lost";


    }
}
