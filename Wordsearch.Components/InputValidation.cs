
namespace Wordsearch.Components
{
    public class InputValidation
    {
        private readonly int maxDifficulty;
        private readonly int minDifficulty;

        public bool ValidateDifficulty(int difficulty)
            => (difficulty > minDifficulty && difficulty <= maxDifficulty);
    }
}
