

namespace Hangman.ConsoleInterface.Tests
{
    public class InputValidationTests
    {

        [Theory]
        [InlineData("t")]
        [InlineData("T")]
        [InlineData("test")]
        [InlineData("Test")]
        public void InputValidationTests_ValidateInput_ReturnsTrue(string input)
        {
            bool returnValue = InputValidation.ValidateInput(input);

            Assert.True(returnValue);

        }

        [Theory]
        [InlineData("1")]
        [InlineData("1test")]
        [InlineData("test1")]
        public void InputValidationTests_ValidateInput_ReturnsFalse(string input)
        {
            bool returnValue = InputValidation.ValidateInput(input);

            Assert.False(returnValue);

        }

        [Theory]
        [InlineData('-')]
        [InlineData(' ')]
        public void InputValidationTests_ValidNonLetterCharacters_ReturnsTrue(char input)
        {
            bool returnValue = InputValidation.ValidNonLetterCharacters(input);

            Assert.True(returnValue);

        }

        [Fact]
        public void InputValidationTests_ValidNonLetterCharacters_ReturnsFalse()
        {
            char input = 'a';
            bool returnValue = InputValidation.ValidNonLetterCharacters(input);

            Assert.False(returnValue);

        }

        [Theory]
        [InlineData(1, 10, 5)]
        [InlineData(1, 10, 1)]
        [InlineData(1, 10, 10)]
        public void InputValidationTests_ValidateUserNumberInput_ReturnsTrue(int lowerBound, int upperBound, int input)
        {
            bool returnValue = InputValidation.ValidateUserNumberInput(lowerBound, upperBound, input);

            Assert.True(returnValue);

        }

        [Theory]
        [InlineData(1, 10, 0)]
        [InlineData(1, 10, 11)]
        public void InputValidationTests_ValidateUserNumberInput_ReturnsFalse(int lowerBound, int upperBound, int input)
        {
            bool returnValue = InputValidation.ValidateUserNumberInput(lowerBound, upperBound, input);

            Assert.False(returnValue);

        }
    }
}
