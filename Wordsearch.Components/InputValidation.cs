
namespace Wordsearch.Components
{
    public class InputValidation
    {
        private readonly int maxDifficulty;
        private readonly int minDifficulty;

        public int[] difficultyBounds { get { return new int[2] { minDifficulty, maxDifficulty }; } }

        public InputValidation() : this(1, 4) { }
        public InputValidation(int maxDifficultyIn, int minDifficultyIn)
        {
            if(maxDifficultyIn <= 0)
            {
                throw new Exception("Value should not be less than 0");
            }
            else
            {
                maxDifficulty = maxDifficultyIn;
            }

            if(minDifficultyIn <= maxDifficulty)
            {
                throw new Exception("Maximum difficulty must be greater than the minimum difficulty");
            }
            else
            {
                minDifficulty = minDifficultyIn;
            }
        }

        public bool ValidateDifficulty(int difficulty)
            => (difficulty > minDifficulty && difficulty <= maxDifficulty);
    }
}
