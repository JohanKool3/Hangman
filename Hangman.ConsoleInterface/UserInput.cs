

using Hangman.Components;

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
    }
}
