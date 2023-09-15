

using Hangman.Components;
using System.Reflection.Metadata.Ecma335;

namespace Hangman.ConsoleInterface
{
    internal class UserInput
    {
        internal static void TakeGuess(Backend backend)
        {
            Console.Write("Enter a word or letter: ");
            string? input = Console.ReadLine();
            Console.Clear();

            if (input == null || !InputValidation.ValidateInput(input)) // Invalid Value inputs
            {
                Console.WriteLine("Must enter a valid value");


            }
            else if (input.Length == 1)
            {
                backend.Input(input[0]);

            }
            else
            {
                int backendWordLength = backend.Word.Length;
                int inputWordLength = input.Length;
                if (inputWordLength == backendWordLength)
                {
                    backend.Input(input);
                }
                else
                {
                    Console.WriteLine($"Word must be {backendWordLength} characters long, current word length: {inputWordLength}");
                }
            }

        }

        internal static int TakeNumberInput(int lowerBound, int upperBound, string? input)
        {
            if(input == null)
            {
                return 0;
            }
            int integerInput = int.Parse(input) ;
            if(InputValidation.ValidateUserNumberInput(lowerBound, upperBound, integerInput))
            {
                return int.Parse(input);
            }
            else
            {
                return 0;
            }

        }
    }
}
