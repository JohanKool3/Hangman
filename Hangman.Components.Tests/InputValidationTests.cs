

namespace Hangman.Components.Tests
{
    public class InputValidation
    {

        [Theory]
        [InlineData('*')]
        [InlineData('1')]
        [InlineData('_')]
        [InlineData(';')]
        public void InputValidation_ValidateInputCharacter_InvalidInput(char input)
        {
            var inputValidation = new Components.InputValidation();

            var result = inputValidation.ValidateInput(input);

            Assert.False(result);
        }

        [Theory]
        [InlineData('a')]
        [InlineData('A')]
        [InlineData('z')]
        [InlineData('Z')]
        public void InputValidation_ValidateInputCharacter_ValidInput(char input)
        {
            var inputValidation = new Components.InputValidation();

            var result = inputValidation.ValidateInput(input);

            Assert.True(result);

        }

        [Theory]
        [InlineData("a1")]
        [InlineData("a*")]
        [InlineData("a_")]
        [InlineData("a;")]
        public void InputValidation_ValidateInputString_InvalidInput(string input)
        {
            var result = Components.InputValidation.ValidateInput(input);

            Assert.False(result);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("ABC")]
        [InlineData("zxy")]
        [InlineData("ZXY")]
        public void InputValidation_ValidateInputString_ValidInput(string input)
        {
            var inputValidation = new Components.InputValidation();

            var result = Components.InputValidation.ValidateInput(input);

            Assert.True(result);
        }
    }
}
