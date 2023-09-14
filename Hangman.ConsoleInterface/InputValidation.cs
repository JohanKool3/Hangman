
namespace Hangman.ConsoleInterface
{
    internal class InputValidation
    {
        internal static bool ValidateInput(string input)
        {
            List<char> characters = input.ToLower().ToList();

            foreach (char character in characters)
            {
                if (!char.IsLetter(character))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
