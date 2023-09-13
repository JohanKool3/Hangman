

namespace Hangman.Components.Tests
{
    public class WordGeneratorTests
    {
        [Fact]
        public void WordGenerator_Constructor_InitialValueIsEmpty()
        {
            DatabaseManager databaseManager = new("localhost", "postgres", "mypassword", "testDatabase");
            WordGenerator generator = new(databaseManager.Connection);

            string word = generator.Word;
            Assert.Equal("", word);
        }

        [Fact]
        public void WordGenerator_GenerateWord_ReturnsString()
        {
            DatabaseManager databaseManager = new("localhost", "postgres", "mypassword", "testDatabase");
            ConfigSettings configSettings = new();
            WordGenerator generator = new(databaseManager.Connection);
            generator.GenerateWord(configSettings);

            string word = generator.Word;
            Assert.IsType<string>(word);
        }
    }
}
