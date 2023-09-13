using Npgsql;

namespace Hangman.Components
{
    public class WordGenerator
    {
        private NpgsqlConnection _connection = new();

        private string word = "";
        public string Word { get { return word; }}

        public WordGenerator(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        internal void GenerateWord(ConfigSettings settings)
        {
            int difficulty = settings.Difficulty;
            var cmd = new NpgsqlCommand($"SELECT content FROM word WHERE difficulty_id = {difficulty} ORDER BY RANDOM() LIMIT 1;",_connection);

            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                word = reader.GetString(0);
                reader.Close();
            }
            else
            {
                throw new Exception("No Data was found");
            }
        }
    }
}
