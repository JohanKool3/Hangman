
namespace Hangman.Components
{
    partial class GameStateHandler
    {
        public int Attempts;
        private int incorrectGuessAmount;
        public int IncorrectGuessAmount { get { return incorrectGuessAmount; } }
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
    }
}
