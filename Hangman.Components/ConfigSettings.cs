
namespace Hangman.Components
{
    public class ConfigSettings
    {

        private int difficulty = 1;
        private static readonly int defaultGuessAmount = 10;
        public int Difficulty { get { return difficulty; } }


        private int maxGuesses;
        public int MaxGuesses { get => maxGuesses; }


        public ConfigSettings() : this(defaultGuessAmount) { }

        public ConfigSettings(int guessAmount)
        {
            UpdateGuessAmount(guessAmount);
        }
        public void UpdateDifficulty(int difficulty)
        {
            if(difficulty <= 4 && difficulty > 0)
            {
                this.difficulty = difficulty;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Value for difficulty must be between 1 and 4");
            }
        }

        public void UpdateGuessAmount(int guessAmount)
        {
            if (ValidateGuessAmount(guessAmount))
            {
                maxGuesses = guessAmount;
            }
            else
            {
                maxGuesses = 7;
            }
        }

        private static bool ValidateGuessAmount(int guessAmount)
            => (guessAmount > 0);



    }
}
