

namespace Hangman.ConsoleInterface
{
    internal class HelperFunctions
    {
        internal static string ConvertCharListToString(List<char> charList)
        {
            string output = "";
            foreach (char character in charList)
            {
                if (character != '\0')
                {
                    output += $"{character} ";
                }
                else
                {
                    output += "_ ";
                }
            }

            return output;
        }

        internal static string ConvertStringListToString(List<string> strList)
        {
            string output = "";
            foreach (string str in strList)
            {
                output += $"{str}, ";
            }

            if (output.Length > 2)
            {
                return output[..^2];
            }
            else
            {
                return output;
            }
        }
    }
}
