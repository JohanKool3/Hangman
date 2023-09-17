

using System.Text;

namespace Hangman.ConsoleInterface
{
    internal static class HelperFunctions
    {
        internal static string ConvertCharListToString(List<char> charList)
        {
            StringBuilder output = new();
            foreach (char character in charList)
            {
                if (character != '\0')
                {
                    output.Append($"{character} ");
                }
                else
                {
                    output.Append("_ ");
                }
            }

            return output.ToString();
        }

        internal static string ConvertStringListToString(List<string> strList)
        {
            StringBuilder output = new();
            foreach (string str in strList)
            {
                output.Append($"{str}, ");
            }

            if (output.Length > 2)
            {
                return output.ToString()[..^2];
            }
            else
            {
                return output.ToString();
            }
        }
    }
}
