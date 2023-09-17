using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ConsoleInterface.Tests
{
    public class HelperFunctionsTests
    {
        [Theory]
        [InlineData(new [] { 'a', 'b', 'c' }, "a b c ")]
        [InlineData(new [] { 'a', 'b', 'c', '\0' }, "a b c _ ")]
        [InlineData(new [] { 'a', 'b', 'c', '\0', '\0' }, "a b c _ _ ")]
        public void ConvertCharListToString_ShouldReturnCorrectString(char[] charArray, string expected)
        {

            List<char> charList = charArray.ToList();

            string actual = HelperFunctions.ConvertCharListToString(charList);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertCharListToString_ShouldNotMatch()
        {
            List<char> charList = new List<char>() { 'a', 'b', 'c' };

            string actual = HelperFunctions.ConvertCharListToString(charList);

            Assert.NotEqual("a b c", actual);
        }

        [Theory]
        [InlineData(new [] { "a", "b", "c" }, "a, b, c")]
        [InlineData(new [] { "a", "b", "c", "d" }, "a, b, c, d")]
        [InlineData(new [] { "a", "b", "c", "d", "e" }, "a, b, c, d, e")]
        public void ConvertStringListToString_ShouldReturnCorrectString(string[] stringArray, string expected)
        {

            List<string> stringList = stringArray.ToList();

            string actual = HelperFunctions.ConvertStringListToString(stringList);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertStringListToString_ShouldNotMatch()
        {
            List<string> stringList = new() { "ab", "bc", "cd" };

            string actual = HelperFunctions.ConvertStringListToString(stringList);

            Assert.NotEqual("a, b, c", actual);
        }
    }
}
