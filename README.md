# Hangman Console Game Project
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/703c87879dba4a33af4c6e621baca3f1)](https://app.codacy.com/gh/JohanKool3/Hangman/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

## Project Overview 

  -This project is a classic game of Hangman played through a command line interface.
  -Players guess letters to uncover a hidden word.
  -Players have 10 guesses to get the word right; otherwise, they lose.
  -Players can choose from different difficulty levels: Easy, Medium, Hard, and Very Hard.

## Game Mechanics 

  -The game starts by selecting a random word based on the chosen difficulty.
  -The word is displayed with underscores representing each letter.
  -Players enter a letter or word to guess.
  -If the letter is in the word, it's revealed in its correct position(s).
  -If the letter is not in the word, a guess is added.
  -Players continue guessing until they win by guessing the word or lose by running out of guesses.

## Difficulty Levels 

  -**Easy**: Common words with many vowels.
  -**Medium**: A bit trickier with a mix of common and less common words.
  -**Hard**: Uncommon words and phrases.
  -**Very Hard**: The toughest level with challenging words

*Note: due to test limitations these difficulties aren't accurate for the test data provided but are
are guide for data that can be added to a production database*

## Backend Setup 

  -To run the game, you need the Docker engine desktop application.
  -A PostgreSQL server is used for unit tests.
  -There is an example client secrets file which outlines the information that needs to be given
in order for a successful connection to the database.

## Unit Tests 

  -The project includes unit tests to ensure code quality and functionality.
  -These tests are based on the PostgreSQL server mentioned earlier.

### Test Database Setup
1.  Navigate to "Hangman.ConsoleInterface/Docker Builder." using a command line (running in administrator)
2.  Execute the "buildAndRun.ps1" command.
