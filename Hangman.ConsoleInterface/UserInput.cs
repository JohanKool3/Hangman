
using Hangman.Components;

namespace Hangman.ConsoleInterface
{
    internal class UserInput
    {
        internal static bool TakeGuess(Backend backend)
        {
            Console.Write("Enter a word or letter: ");
            string? input = Console.ReadLine();
            Console.Clear();
            return TakeGuess(backend, input);

        }

        internal static bool TakeGuess(Backend backend,string? input)
        {
            if (input == null || !InputValidation.ValidateInput(input)) // Invalid Value inputs
            {
                Console.WriteLine("Must enter a valid value");
                return false;
            }
            else if (input.Length == 1)
            {
                backend.Input(input[0]);
                return true;

            }
            else
            {
                int backendWordLength = backend.Word.Length;
                int inputWordLength = input.Length;
                if (inputWordLength == backendWordLength)
                {
                    backend.Input(input);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Word must be {backendWordLength} characters long, current word length: {inputWordLength}");
                    return false;
                }
            }
        }

        internal static int TakeNumberInput(int lowerBound, int upperBound, string? input)
        {
            if(input == null)
            {
                return 0;
            }
            int integerInput = -1;
            try
            {
                integerInput = int.Parse(input);
            }
            catch
            {
                return 0;
            }
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
