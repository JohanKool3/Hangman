

namespace Wordsearch.Components.Tests
{
    public class DatabaseManagerTests
    {
        [Fact]
        public void DatabaseManager_ConstructorWorks_Suceeds()
        {

            DatabaseManager dbManager = new("localhost","postgres","mypassword","testDatabase");
            Assert.NotNull(dbManager);

        }
        [Fact]
        public void DatabaseManager_ConstructorWorks_Fails()
        {
            Assert.Throws<Exception>(() => new DatabaseManager("localhost", "postgres", "mypassword", "notAValidDatbase"));
        }

        [Fact]
        public void DatabaseManager_FetchIntegerFromDatabase_Suceeds()
        {
            DatabaseManager databaseManager = new("localhost", "postgres", "mypassword", "testDatabase");
            int expected = 1;
            int actual = databaseManager.GetIntegerFromDatabase("SELECT difficulty_id FROM difficulty LIMIT 1;");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DatabaseManager_FetchStringFromDatabase_Suceeds()
        {
            DatabaseManager databaseManager = new("localhost", "postgres", "mypassword", "testDatabase");
            string expected = "easy";
            string actual = databaseManager.GetStringFromDatabase("SELECT tag FROM difficulty LIMIT 1;").ToLower();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DatabaseManager_ConfigureNewConnection_Suceeds()
        {
            DatabaseManager databaseManager = new("localhost", "postgres", "mypassword", "testDatabase");
            databaseManager.ConfigureNewConnection("localhost", "postgres", "mypassword", "postgres");

            Assert.NotNull(databaseManager);

        }

        [Theory]
        [InlineData("nohost","postgres","mypassword","testDatabase")]
        [InlineData("localhost","invalidusername","mypassword","testDatabase")]
        [InlineData("localhost","postgres","notmypassword","testDatabase")]
        [InlineData("localhost","postgres","mypassword","noDatabaseHere")]
        public void DatabaseManager_ConfigureNewConnection_Fails(string host, string username, string password, string database)
        {
            DatabaseManager databaseManager = new("localhost", "postgres", "mypassword", "testDatabase");
            Assert.Throws<Exception>(() => databaseManager.ConfigureNewConnection(host, username, password, database));
        }

    }
}
