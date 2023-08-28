using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Wordsearch.Components;

namespace Wordsearch
{
    public class Program
    {
        // Components
        private static Backend? backend;

        public static void Main(string[] args)
        {
            // Fetch the information from the user secrets file
            var config = new ConfigurationBuilder()
                        .AddUserSecrets<Program>()
                        .Build();

            // Setup Database Connection (defaults are examples)
            string _host = config["Host"] ?? "localhost";
            string _username = config["Username"] ?? "username";
            string _password = config["Password"] ?? "password";
            string _database = config["Database"] ?? "database";

            try
            {
                backend = new(_host, _username, _password, _database);
                Console.WriteLine(backend.Word);
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine("Incorrect configuration for user secrets", e);
            }
        }   
    }
}