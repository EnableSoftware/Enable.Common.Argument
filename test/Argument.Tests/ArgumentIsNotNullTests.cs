using System;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsNotNullTests
    {
        [Fact]
        public void IsNotNull_DoesNotThrowIfArgumentNotNull()
        {
            // Arrange
            object argument = new object();

            try
            {
                // Act
                Argument.IsNotNull(argument, nameof(argument));
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsNotNull_ThrowsIfArgumentIsNull()
        {
            // Arrange
            object argument = null;

            // Act
            var action = new Action(() =>
            {
                Argument.IsNotNull(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void IsNotNull_ThrowsWithExpectedMessage()
        {
            // Arrange
            var fixture = new Fixture();
            var argumentName = fixture.Create<string>();
            var expectedMessage = "Value cannot be null." + Environment.NewLine + "Parameter name: " + argumentName;
            Exception exception = null;

            // Act
            try
            {
                Argument.IsNotNull(null, argumentName);
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
