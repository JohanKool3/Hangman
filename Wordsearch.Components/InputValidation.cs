
namespace Wordsearch.Components
{
    public class InputValidation
    {
        private int maxDifficulty;
        private int minDifficulty;

        public bool ValidateDifficulty(int difficulty)
            => (difficulty > minDifficulty && difficulty <= maxDifficulty);
    }
}
