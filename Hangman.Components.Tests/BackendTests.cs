
namespace Hangman.Components.Tests
{
    public class BackendTests
    {

        [Fact]
        public void Backend_Constructor_DoesNotThrowException()
        {
            Backend backend = new("localhost", "postgres", "mypassword", "testDatabase");
            Assert.NotNull(backend);
        }
        [Fact]
        public void Backend_Constructor_ThrowsException()
        {
            Assert.Throws<Exception>(() => new Backend("localhost", "postgres", "notmypassword", "testDatabase"));
        }

        [Fact]
        public void Backend_SetNewWord_DoesNotThrowException()
        {
            Backend backend = new("localhost", "postgres", "mypassword", "testDatabase");
            string initialWord = backend.Word;
            backend.SetNewWord();
            string newWord = backend.Word;

            Assert.NotEqual(initialWord, newWord);

        }
    }
}
