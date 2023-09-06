

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

    }
}
