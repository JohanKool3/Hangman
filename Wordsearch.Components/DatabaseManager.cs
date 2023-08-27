using Npgsql;
using System.Data.Common;

namespace Wordsearch.Components
{
    internal class DatabaseManager
    {

        private NpgsqlConnection _connection = new NpgsqlConnection();
        internal NpgsqlConnection Connection { get { return _connection; } }

        internal DatabaseManager(string host, string username, string password, string database)
        {
            SetupDatabaseConnection(host, username, password, database);
        }

        private void SetupDatabaseConnection(string host, string username, string password, string database)
        {
            string connString = $"Host={host};Username={username};Password={password};Database={database}";
            _connection = new NpgsqlConnection(connString);
            _connection.Open();
        }

        internal void ConfigureNewConnection(string host, string username, string password, string database)
            => SetupDatabaseConnection(host, username, password, database);

        internal int GetIntegerFromDatabase(string queryString)
        {
            int output;
            var cmd = new NpgsqlCommand(queryString, _connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            output = reader.GetInt32(0);
            reader.Close();

            return output;
        }

        internal string GetStringFromDatabase(string queryString)
        {
            string output;
            var cmd = new NpgsqlCommand(queryString, _connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            output = reader.GetString(0);
            reader.Close();

            return output;
        }
    }
}
