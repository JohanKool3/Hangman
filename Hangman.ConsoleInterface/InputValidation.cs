
namespace Hangman.ConsoleInterface
{
    internal class InputValidation
    {
        internal static bool ValidateInput(string input)
        {
            List<char> characters = input.ToLower().ToList();

            foreach (char character in characters)
            {
                if (!char.IsLetter(character) && !ValidNonLetterCharacters(character))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool ValidNonLetterCharacters(char character)
        {
            List<char> validCharacters = new (){ '-', ' ' };
            return validCharacters.Contains(character);
        }

        internal static bool ValidateUserNumberInput(int lowerBound, int upperBound, int input) => input >= lowerBound && input <= upperBound;
    }
}
