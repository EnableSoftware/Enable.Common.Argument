using System;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsInRangeTests
    {
        [Fact]
        public void IsInRange_DoesNotThrowIfConditionTrue()
        {
            // Arrange
            var fixture = new Fixture();
            var paramName = fixture.Create<string>();

            try
            {
                // Act
                Argument.IsInRange(true, paramName);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsInRange_ThrowsIfConditionFalse()
        {
            // Arrange
            var fixture = new Fixture();
            var paramName = fixture.Create<string>();

            // Act
            var action = new Action(() =>
            {
                Argument.IsInRange(false, paramName);
            });

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void IsInRange_ThrowsWithExpectedMessage()
        {
            // Arrange
            var fixture = new Fixture();
            var argumentName = fixture.Create<string>();
            var expectedMessage = "Specified argument was out of the range of valid values." + Environment.NewLine + "Parameter name: " + argumentName;
            Exception exception = null;

            // Act
            try
            {
                Argument.IsInRange(false, argumentName);
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
