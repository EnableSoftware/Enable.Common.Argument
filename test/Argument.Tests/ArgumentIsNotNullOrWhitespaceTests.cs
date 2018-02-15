using System;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsNotNullOrWhiteSpaceTests
    {
        [Theory]
        [InlineData("foo")]
        [InlineData("foo bar")]
        [InlineData(" foo bar")]
        [InlineData("foo bar ")]
        [InlineData(" foo bar ")]
        public void IsNotNullOrWhiteSpace_DoesNotThrowIfArgumentNotNullOrWhiteSpace(string argument)
        {
            // Arrange
            var fixture = new Fixture();

            try
            {
                // Act
                Argument.IsNotNullOrWhiteSpace(argument, nameof(argument));
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsNotNullOrWhiteSpace_ThrowsIfArgumentIsNull()
        {
            // Arrange
            string argument = null;

            // Act
            var action = new Action(() =>
            {
                Argument.IsNotNullOrWhiteSpace(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void IsNotNullOrWhiteSpace_ThrowsIfArgumentIsEmpty()
        {
            // Arrange
            var argument = string.Empty;

            // Act
            var action = new Action(() =>
            {
                Argument.IsNotNullOrWhiteSpace(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("\r\n")]
        [InlineData("\t")]
        public void IsNotNullOrWhiteSpace_ThrowsIfArgumentIsWhiteSpace(string argument)
        {
            // Act
            var action = new Action(() =>
            {
                Argument.IsNotNullOrWhiteSpace(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(null, "Value cannot be null.")]
        [InlineData("", "Value cannot be empty.")]
        [InlineData("   ", "Value cannot be white space.")]
        public void IsNotNullOrWhiteSpace_ThrowsWithExpectedMessage(string argument, string expectedMessageFirstLine)
        {
            // Arrange
            var fixture = new Fixture();
            var paramName = fixture.Create<string>();
            var expectedMessage = expectedMessageFirstLine + Environment.NewLine + "Parameter name: " + paramName;
            Exception exception = null;

            // Act
            try
            {
                Argument.IsNotNullOrWhiteSpace(argument, paramName);
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
