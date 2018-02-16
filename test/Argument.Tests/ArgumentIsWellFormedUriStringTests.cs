using System;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsWellFormedUriStringTests
    {
        [Fact]
        public void IsWellFormedUriStringTests_DoesNotThrowIfWellFormed()
        {
            // Arrange
            var fixture = new Fixture();
            var argument = fixture.Create<Uri>().ToString();

            try
            {
                // Act
                Argument.IsWellFormedUriString(argument, nameof(argument));
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsWellFormedUriStringTests_ThrowsIfNull()
        {
            // Arrange
            string argument = null;

            var action = new Action(() =>
            {
                Argument.IsWellFormedUriString(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("c:\\directory\filename", "Specified argument must be a valid absolute URI.", UriKind.Absolute)]
        [InlineData("www.test.com/path/file", "Specified argument must be a valid absolute URI.", UriKind.Absolute)]
        [InlineData("2017.09.06_13:37:41", "Specified argument must be a valid relative URI.", UriKind.Relative)]
        [InlineData("www.test.com/path???/file ", "Specified argument must be a valid URI.", UriKind.RelativeOrAbsolute)]
        public void IsWellFormedUriStringTests_MalformedThrowsWithExpectedMessage(string argument, string message, UriKind uriKind)
        {
            // Arrange
            var fixture = new Fixture();
            var argumentName = fixture.Create<string>();
            var expectedMessage = message + Environment.NewLine + "Parameter name: " + argumentName;
            Exception exception = null;

            try
            {
                // Act
                Argument.IsWellFormedUriString(argument, argumentName, uriKind);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
