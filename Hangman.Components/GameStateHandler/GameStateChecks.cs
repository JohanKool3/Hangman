
namespace Hangman.Components
{
    partial class GameStateHandler
    {
        internal bool IsCorrectGuessString(string input)
             => (input == word) || AlternateCorrectWord(input);
        internal bool IsCorrectGuessChar(char input)
            => wordLetters.Any(character => (character == input));

        internal void MaxGuessChecks()
        {
            if (incorrectGuessAmount >= maxGuesses)
            {
                complete = true;
            }
        }

        internal bool AlternateCorrectWord(string input)
        {
            int indexOfSpecialCharacter = -1;
            if (input.Contains('-'))
            {
                indexOfSpecialCharacter = input.IndexOf('-');
            }
            else
            {
                indexOfSpecialCharacter = input.IndexOf(' ');
            }

            // If the input is supposed to have a hyphen but has a space, allow it. Vice versa
            if (indexOfSpecialCharacter != -1)
            {
                if (word.Contains('-'))
                {
                    return (input.Replace(' ', '-') == word);
                }
                else
                {
                    return (input.Replace('-', ' ') == word);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
