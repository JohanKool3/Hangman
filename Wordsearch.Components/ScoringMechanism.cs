using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch.Components
{
    
    public class ScoringMechanism
    {
        private InputValidation validator = new();
        private ConfigSettings settings = new();
        private WordGenerator wordGenerator = new();


        public string Word { get => wordGenerator.Word; }

        /// <summary>
        /// Main backend object to use to interact with front end elements
        /// </summary>
        public ScoringMechanism(string host, string username, string password, string database) 
        { 
            wordGenerator.SetupDatabaseConnection(host, username, password, database);
            wordGenerator.GenerateWord(settings.Difficulty);
        }

        public void SetNewWord()
        {
            if (validator.ValidateDifficulty(settings.Difficulty))
            {
                wordGenerator.GenerateWord(settings.Difficulty);
            }
            else
            {
                // TODO: Handle invalid inputs
            }
        }

        public void SetNewDifficulty(int newDifficulty)
        {
            if (validator.ValidateDifficulty(newDifficulty))
            {
                settings.UpdateDifficulty(newDifficulty);
            }
        }

        public void CustomizeGuessAmount(int guesses)
        {
            settings.UpdateGuessAmount(guesses);
        }
    }
}
