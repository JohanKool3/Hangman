﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch.Components
{
    internal class ConfigSettings
    {

        private int difficulty = 1;
        public int Difficulty { get { return difficulty; } }


        private int maxGuesses;
        public int MaxGuesses { get => maxGuesses; }


        public ConfigSettings() : this(7) { }

        public ConfigSettings(int guessAmount)
        {
            if(ValidateGuessAmount(guessAmount))
            {
                maxGuesses = guessAmount;
            }
        }
        public void UpdateDifficulty(int difficulty)
        {
            if(difficulty <= 4 && difficulty >= 0)
            {
                this.difficulty = difficulty;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Value for difficulty must be between 1 and 4");
            }
        }

        public void UpdateGuessAmount(int guessAmount)
        {
            if(ValidateGuessAmount(guessAmount))
            {
                maxGuesses = guessAmount;
            }
        }

        private bool ValidateGuessAmount(int guessAmount)
            => (guessAmount < 0);

        

    }
}
