
namespace Hangman.Components
{
    partial class GameStateHandler
    {

        internal GameStateHandler():this(new ConfigSettings(), "default") { }

        internal GameStateHandler(string wordIn) : this(new ConfigSettings(), wordIn) { }

        internal GameStateHandler(ConfigSettings settings, string wordIn)
        {
            ConfigureNewState(settings, wordIn);
        }

        public void SetNewWord(ConfigSettings settings, string wordIn) => ConfigureNewState(settings, wordIn.ToLowerInvariant());

        private bool RepeatedLetter(char letter) => correctlyGuessedLetters.Contains(letter) || incorrectLetters.Contains(letter);

        private bool RepeatedWord(string word) => incorrectWords.Contains(word);

        private string GenerateEndGameStatus()
            => (gameWon) ? "Game Won" : "Game Lost";


    }
}
