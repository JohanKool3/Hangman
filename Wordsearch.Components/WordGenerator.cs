using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch.Components
{
    public class WordGenerator
    {
        private NpgsqlConnection? _connection;

        private string word = "";
        public string Word { get { return word; }}


        internal void SetupDatabaseConnection(string host, string username, string password, string database)
        {
            string connString = $"Host={host};Username={username};Password={password};Database={database}";
            _connection = new NpgsqlConnection(connString);
            _connection.Open();
        }

        internal void GenerateWord(int difficulty)
        {
            var cmd = new NpgsqlCommand($"SELECT content FROM word WHERE difficulty_id = {difficulty} ORDER BY RANDOM() LIMIT 1;",_connection);

            var reader = cmd.ExecuteReader();
            
            if(reader.Read())
            {
                word = reader.GetString(0);
            }
            else
            {
                throw new Exception("No Data was found");
            }
        }
    }
}
