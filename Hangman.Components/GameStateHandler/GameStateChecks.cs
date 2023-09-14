using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (currentGuesses >= maxGuesses)
            {
                complete = true;
            }
        }
    }
}
