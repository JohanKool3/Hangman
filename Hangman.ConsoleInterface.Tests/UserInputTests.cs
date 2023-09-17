using Hangman.Components;

namespace Hangman.ConsoleInterface.Tests
{
    public class UserInputTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        public void UserInput_TakeGuess_ReturnsTrue(string input)
        {
            // Arrange
            var backend = new Backend("localhost", "postgres", "mypassword", "testDatabase");

            // Act
            var result = UserInput.TakeGuess(backend, input);

            Assert.True(result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("*")]
        public void UserInput_TakeGuess_ReturnsFalse(string input)
        {
            // Arrange
            var backend = new Backend("localhost", "postgres", "mypassword", "testDatabase");

            // Act
            var result = UserInput.TakeGuess(backend, input);

            Assert.False(result);
        }

        [Theory]
        [InlineData(1, 3, "2", 2)]
        [InlineData(1, 3, "3", 3)]
        [InlineData(1, 3, "1", 1)]
        public void UserInput_TakeNumberInput_ReturnsNumber(int lowerBound, int upperBound, string input, int expected)
        {
            // Act
            var result = UserInput.TakeNumberInput(lowerBound, upperBound, input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 3, "4")]
        [InlineData(1, 3, "0")]
        [InlineData(1, 3, "a")]
        public void UserInput_TakeNumberInput_ReturnsZero(int lowerBound, int upperBound, string input)
        {
            // Act
            var result = UserInput.TakeNumberInput(lowerBound, upperBound, input);

            Assert.Equal(0, result);
        }
    }
}
