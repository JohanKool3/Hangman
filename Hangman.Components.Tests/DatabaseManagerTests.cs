

namespace Wordsearch.Components.Tests
{
    public class DatabaseManagerTests
    {
        [Theory]
        [InlineData("localhost","postgres","mypassword","testDatabase", true)]
        [InlineData("localhost","postgres","notmypassword","testDatabase", false)]
        public void DatabaseManager_ConstructorWorks_MatchesExpected(string host, string username, string password, string database, bool passes)
        {
            if (passes)
            {
                DatabaseManager dbManager = new(host, username, password, database);
                Assert.NotNull(dbManager);
            }
            else
            {
                Assert.Throws<Exception>(() => new DatabaseManager(host, username, password, database));
            }
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

            Assert.Null(() => databaseManager.ConfigureNewConnection("localhost", "postgres", "mypassword", "postgres"));

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
