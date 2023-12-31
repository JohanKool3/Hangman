﻿using Npgsql;

namespace Hangman.Components
{
    internal class DatabaseManager
    {

        private NpgsqlConnection _connection = new NpgsqlConnection();
        internal NpgsqlConnection Connection { get { return _connection; } }

        internal DatabaseManager(string host, string username, string password, string database)
        {
            try
            {
                SetupDatabaseConnection(host, username, password, database);
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid database Setup", ex);
            }
        }

        private void SetupDatabaseConnection(string host, string username, string password, string database)
        {
            try
            {
                string connString = $"Host={host};Username={username};Password={password};Database={database}";
                _connection = new NpgsqlConnection(connString);
                _connection.Open();
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid database Setup", ex);
            }
        }

        internal void ConfigureNewConnection(string host, string username, string password, string database)
            => SetupDatabaseConnection(host, username, password, database);

        internal async Task<int> GetIntegerFromDatabase(string queryString)
        {
            int output;
            var cmd = new NpgsqlCommand(queryString, _connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            output = reader.GetInt32(0);
            reader.Close();

            return output;
        }

        internal async Task<string> GetStringFromDatabase(string queryString)
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
