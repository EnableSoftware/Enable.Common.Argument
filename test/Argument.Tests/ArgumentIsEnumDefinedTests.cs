using System;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ArgumentIsEnumDefinedTests
    {
        [Fact]
        public void IsEnumDefined_DoesNotThrowIfEnumDefined()
        {
            // Arrange
            var fixture = new Fixture();
            var argument = fixture.Create<DayOfWeek>();

            try
            {
                // Act
                Argument.IsEnumDefined(argument, nameof(argument));
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsEnumDefined_DoesNotThrowIfNull()
        {
            // Arrange
            var fixture = new Fixture();
            DayOfWeek? argument = null;

            try
            {
                // Act
                Argument.IsEnumDefined(argument, nameof(argument));
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, "Unexpected exception thrown: " + ex.Message);
            }
        }

        [Fact]
        public void IsEnumDefined_ThrowsIfEnumNotDefined()
        {
            // Arrange
            var argument = GetInvalidEnumValue();

            // Act
            var action = new Action(() =>
            {
                Argument.IsEnumDefined(argument, nameof(argument));
            });

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void IsEnumDefined_ThrowsWithExpectedMessage()
        {
            // Arrange
            var fixture = new Fixture();
            var argument = GetInvalidEnumValue();
            var argumentName = fixture.Create<string>();
            var expectedMessage = "Specified argument was out of the range of valid values for enum 'DayOfWeek'." + Environment.NewLine + "Parameter name: " + argumentName;
            Exception exception = null;

            // Act
            try
            {
                Argument.IsEnumDefined(argument, argumentName);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        private DayOfWeek GetInvalidEnumValue()
        {
            var fixture = new Fixture();
            return (DayOfWeek)(Enum.GetValues(typeof(DayOfWeek)).Length + fixture.Create<int>());
        }
    }
}
