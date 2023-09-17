
namespace Hangman.Components
{
    public class InputValidation
    {
        private readonly int maxDifficulty;
        private readonly int minDifficulty;

        public int[] DifficultyBounds { get { return new [] { minDifficulty, maxDifficulty }; } }

        public InputValidation() : this(1, 4) { }
        public InputValidation(int minDifficultyIn, int maxDifficultyIn)
        {
            if(maxDifficultyIn <= 0)
            {
                throw new Exception("Value should not be less than 0");
            }
            else
            {
                maxDifficulty = maxDifficultyIn;
            }

            if(minDifficultyIn >= maxDifficulty)
            {
                throw new Exception("Maximum difficulty must be greater than the minimum difficulty");
            }
            else
            {
                minDifficulty = minDifficultyIn;
            }
        }

        public bool ValidateDifficulty(int difficulty)
            => (difficulty >= minDifficulty && difficulty <= maxDifficulty);
        /// <summary>
        /// Returns True if the input is a valid word
        /// </summary>
        /// <param name="wordIn"></param>
        /// <returns></returns>
        internal static bool ValidateInput(string wordIn)
        {
            List<char> characters = wordIn.ToLowerInvariant().ToList();

            foreach (char character in characters)
            {
                if(!char.IsLetter(character) && !IsValidCharacter(character))
                {
                    return false;
                }
            }
            return true;
        }
        internal bool ValidateInput(char letterIn)
            => IsValidCharacter(letterIn);

        private static bool IsValidCharacter(char character)
        {
            return character switch
            {
                '-' => true,
                ' ' => true,
                _ => char.IsLetter(character),
            };
        }
    }
}
