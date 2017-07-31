using System;
using Ploeh.AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsNotNullOrWhiteSpaceTests
    {
        [Fact]
        public void IsNotNullOrWhiteSpace_DoesNotThrowIfArgumentNotNullOrWhiteSpace()
        {
            // Arrange
            var fixture = new Fixture();
            string argument = fixture.Create<string>();

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
            Assert.Throws(typeof(ArgumentNullException), action);
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
            Assert.Throws(typeof(ArgumentException), action);
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
            Assert.Throws(typeof(ArgumentException), action);
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
