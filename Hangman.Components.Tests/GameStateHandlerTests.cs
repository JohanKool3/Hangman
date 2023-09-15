

namespace Hangman.Components.Tests
{
    public class GameStateHandlerTests
    {

        [Fact]
        public void GameStateHandler_ConstructorDefaultValues_GuessAmountEqual10()
        {
            GameStateHandler handler = new();
            Assert.Equal(10, handler.MaxGuesses);
        }
        [Fact]
        public void GameStateHandler_ConstructorDefaultValue_WordLengthEqual7()
        {
            GameStateHandler handler = new();
            char[] chars = handler.CorrectlyGuessedLetters;
            Assert.Equal(7, chars.Length);
        }

        [Fact]
        public void GameStateHandler_ConstructorWithWord_GuessAmountEqual10()
        {
            GameStateHandler handler = new("test");
            Assert.Equal(10, handler.MaxGuesses);
        }

        [Fact]
        public void GameStateHandler_ConstructorWithWord_WordLengthEqual4()
        {
            GameStateHandler handler = new("test");
            char[] chars = handler.CorrectlyGuessedLetters;
            Assert.Equal(4, chars.Length);
        }

        [Fact]
        public void GameStateHandler_ConstructorWithWordAndSettings_GuessAmountEqual5()
        {
            ConfigSettings settings = new(5);
            GameStateHandler handler = new(settings, "test");
            Assert.Equal(5, handler.MaxGuesses);
        }

        [Fact]
        public void GameStateHandler_ConstructorWithWordAndSettings_WordLengthEqual4()
        {
            ConfigSettings settings = new(5);
            GameStateHandler handler = new(settings, "test");
            char[] chars = handler.CorrectlyGuessedLetters;
            Assert.Equal(4, chars.Length);
        }

        [Theory]
        [InlineData("C", 1)]
        [InlineData("Cheese", 6)]
        [InlineData("Test", 4)]
        public void GameStateHandler_SetNewWord_LengthMatchesExpected(string word, int expectedLength)
        {
            GameStateHandler handler = new();
            handler.SetNewWord(new ConfigSettings(), word);
            char[] chars = handler.CorrectlyGuessedLetters;
            Assert.Equal(expectedLength, chars.Length);
        }

        [Theory]
        [InlineData("test",'t', 0)]
        [InlineData("cheese",'h',1)]
        [InlineData("test",'s',2)]
        public void GameStateHandler_CharacterInput_MatchesExpectedPosition(string word, char letterGuess, int expectedPosition)
        {
            GameStateHandler handler = new(word);

            handler.Input(letterGuess);
            char[] chars = handler.CorrectlyGuessedLetters;

            Assert.Equal(letterGuess, chars[expectedPosition]);

        }

        [Theory]
        [InlineData("test",'a', 0)]
        [InlineData("cheese",'b',1)]
        [InlineData("test",'c',2)]
        public void GameStateHandler_CharacterInput_ShouldNotMatchExpectedPosition(string word, char letterGuess, int expectedPosition)
        {
            GameStateHandler handler = new(word);
            handler.Input(letterGuess);
            char[] chars = handler.CorrectlyGuessedLetters;

            Assert.NotEqual(letterGuess, chars[expectedPosition]);
        }

        [Fact]
        public void GameStateHandler_CharacterInput_IncrementGuessAmount()
        {
            GameStateHandler handler = new("test");
            handler.Input('a');
            Assert.Equal(1, handler.IncorrectGuessAmount);
        }

        [Fact]
        public void GameStateHandler_CharacterInput_IncorrectLettersAddedTo()
        {
            GameStateHandler handler = new("test");
            handler.Input('a');

            Assert.Contains('a', handler.IncorrectLetters);
        }

        [Fact]
        public void GameStateHandler_CharacterInput_IncorrectLettersLengthEqual1()
        {
            GameStateHandler handler = new("test");
            handler.Input('a');
            List<char> incorrectLetters = handler.IncorrectLetters;

            Assert.Single(incorrectLetters);
        }


        [Fact]
        public void GameStateHandler_StringInput_EqualsWord()
        {
            GameStateHandler handler = new("test");
            handler.Input("test");
            string gameWon = "Game Won";

            Assert.Equal(gameWon, handler.GameStatus);
        }

        [Fact]
        public void GameStateHandler_StringInput_IncorrectGuessesIncrement()
        {
            GameStateHandler handler = new("test");
            handler.Input("nest");

            Assert.Equal(1, handler.IncorrectGuessAmount);
        }

        [Fact]
        public void GameStateHandler_StringInput_IncorrectWordsAddedTo()
        {
            GameStateHandler handler = new("test");
            handler.Input("nest");

            Assert.Contains("nest", handler.IncorrectWords);
        }

        [Fact]
        public void GameStateHandler_StringInput_IncorrectWordsLengthEqual1()
        {
            GameStateHandler handler = new("test");
            handler.Input("nest");

            Assert.Single(handler.IncorrectWords);
        }

        [Fact]
        public void GameStateHandler_GameStatus_ShouldEqualRunning()
        {
            GameStateHandler handler = new("test");
            string running = "Running";

            Assert.Equal(running, handler.GameStatus);
        }

        [Fact]
        public void GameStateHandler_GameStatus_ShouldEqualWon()
        {
            GameStateHandler handler = new("test");
            handler.Input("test");
            string won = "Game Won";

            Assert.Equal(won, handler.GameStatus);
        }

        [Fact]
        public void GameStatusHandler_GameStatus_ShouldEqualLost()
        {
            ConfigSettings configSettings = new(1);
            GameStateHandler handler = new(configSettings, "test");
            handler.Input("nest");
            string lost = "Game Lost";

            Assert.Equal(lost, handler.GameStatus);
        }

        [Theory]
        [InlineData("beans-bread", 5, '-')]
        [InlineData("multiple lines",8, ' ')]
        public void GameStateHandler_CorrectlyGuessedLetters_ShouldMatchExpectedValues(string word, int expectedIndex, char expectedChar)
        {
            GameStateHandler handler = new(word);

            char[] chars = handler.CorrectlyGuessedLetters;

            Assert.Equal(expectedChar, chars[expectedIndex]);
        }
    }
}
