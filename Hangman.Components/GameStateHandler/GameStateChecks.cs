
namespace Hangman.Components
{
    partial class GameStateHandler
    {
        internal bool IsCorrectGuessString(string input)
             => (input == word);
        internal bool IsCorrectGuessChar(char input)
            => wordLetters.Any(character => (character == input));

        internal void MaxGuessChecks()
        {
            if (incorrectGuessAmount >= maxGuesses)
            {
                complete = true;
            }
        }
    }
}
