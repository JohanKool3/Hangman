
namespace Hangman.Components.Tests
{
    public class ConfigSettingsTests
    {
        [Theory]
        [InlineData(9, 9)]
        [InlineData(-1, 7)]
        public void ConfigSettings_Initialization_MatchExpected(int guessAmount, int expectedOutcome)
        {
            ConfigSettings settings = new(guessAmount);
            Assert.Equal(expectedOutcome, settings.MaxGuesses);
        }

        [Theory]
        [InlineData(9, 9)]
        [InlineData(-1, 7)]
        public void ConfigSettings_UpdateGuessAmount_MatchExpected(int newAmount, int expectedOutcome)
        {
            ConfigSettings settings = new();

            settings.UpdateGuessAmount(newAmount);

            Assert.Equal(expectedOutcome, settings.MaxGuesses);
        }

        [Fact]
        public void ConfigSettings_DifficultyInitialization_Equals1()
        {
            ConfigSettings settings = new();
            Assert.Equal(1, settings.Difficulty);
        }

        [Fact]
        public void ConfigSettings_DifficultyUpdate_Passes()
        {
            ConfigSettings settings = new();

            settings.UpdateDifficulty(1);
            Assert.Equal(1, settings.Difficulty);
        }

        [Fact]
        public void ConfigSettings_DifficultyUpdate_Fails()
        {
            ConfigSettings settings = new();

            Assert.Throws<ArgumentOutOfRangeException>(() => settings.UpdateDifficulty(0));
        }

        [Theory]
        [InlineData(9, 9)]
        [InlineData(-1, 7)]
        public void ConfigSettings_UpdateMaxGuesses_MatchExpected(int newAmount, int expectedOutcome)
        {
            ConfigSettings settings = new();
            settings.UpdateGuessAmount(newAmount);

            Assert.Equal(expectedOutcome, settings.MaxGuesses);
        }
    }
}
