
namespace Wordsearch.Components.Tests
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

        [Theory]
        [InlineData(2, 2)]
        [InlineData(-1, 1)]
        public void ConfigSettings_DifficultyUpdate_MatchExpected(int newAmount, int expectedOutcome)
        {
            ConfigSettings settings = new();

            try
            {
                settings.UpdateDifficulty(newAmount);
                Assert.Equal(expectedOutcome, settings.Difficulty);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                if (newAmount < 0)
                {
                    Assert.True(true);
                }
                else
                {
                    Assert.True(false, ex.Message);
                }
            }
            
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
