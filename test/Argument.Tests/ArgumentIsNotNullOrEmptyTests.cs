using System;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsNotNullOrEmptyTests
    {
        [Fact]
        public void IsNotNullOrEmpty_DoesNotThrowIfArgumentNotNullOrEmpty()
        {
            // Arrange
            var fixture = new Fixture();
            string argument = fixture.Create<string>();

            try
            {
                // Act
                Argument.IsNotNullOrEmpty(argument, nameof(argument));
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsNotNullOrEmpty_ThrowsIfArgumentIsNull()
        {
            // Arrange
            string argument = null;

            // Act
            var action = new Action(() =>
            {
                Argument.IsNotNullOrEmpty(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void IsNotNullOrEmpty_ThrowsIfArgumentIsEmpty()
        {
            // Arrange
            var argument = string.Empty;

            // Act
            var action = new Action(() =>
            {
                Argument.IsNotNullOrEmpty(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(null, "Value cannot be null.")]
        [InlineData("", "Value cannot be empty.")]
        public void IsNotNullOrEmpty_ThrowsWithExpectedMessage(string argument, string expectedMessageFirstLine)
        {
            // Arrange
            var fixture = new Fixture();
            var paramName = fixture.Create<string>();
            var expectedMessage = expectedMessageFirstLine + Environment.NewLine + "Parameter name: " + paramName;
            Exception exception = null;

            // Act
            try
            {
                Argument.IsNotNullOrEmpty(argument, paramName);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
