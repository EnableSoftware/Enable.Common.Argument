using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Enable.Common
{
    public class ArgumentTests
    {
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
            Assert.Throws(typeof(ArgumentNullException), action);
        }
    }
}
