

namespace Hangman.Components
{
    partial class GameStateHandler
    {

        private void ConfigureNewState(ConfigSettings settings, string wordIn)
        {
            Attempts = 0;
            gameWon = false;
            complete = false;
            incorrectGuessAmount = 0;
            maxGuesses = settings.MaxGuesses;
            ConfigureWordFields(wordIn);
            word = wordIn.ToLowerInvariant();
        }

        private void ConfigureWordFields(string word)
        {
            int wordLength = word.Length;
            incorrectLetters = new();
            incorrectWords = new();

            correctlyGuessedLetters = new char[wordLength];
            wordLetters = word.Select(letter => letter).ToArray();
            ConfigureNonLetterCharacters();
        }

        private List<int> GetIndexOfLetter(char letter)
        {
            List<int> output = new();

            for (int i = 0; i < wordLetters.Length; i++)
            {
                if (wordLetters[i] == letter)
                {
                    output.Add(i);
                }
            }
            return output;
        }

        private void ConfigureNonLetterCharacters()
        {
            foreach (int index in GetIndexOfLetter(' '))
            {
                correctlyGuessedLetters[index] = ' ';
            }

            foreach (int index in GetIndexOfLetter('-'))
            {
                correctlyGuessedLetters[index] = '-';
            }
        }
    }
}
