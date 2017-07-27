using System;

namespace Enable.Common
{
    /// <summary>
    /// This helper class is used to enforce argument conditions at runtime.
    /// Use of this class helps to document method or constructor preconditions in a terse manner.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Throws an exception if the enum value is not defined.
        /// </summary>
        public static void IsEnumDefined(Enum value, string paramName)
        {
            if (value == null)
            {
                return;
            }

            var isDefined = Enum.IsDefined(value.GetType(), value);

            if (!isDefined)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if range predicate evaluates to false.
        /// </summary>
        public static void IsInRange(bool condition, string paramName)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if string argument is null or empty.
        /// </summary>
        public static void IsNotNullOrEmpty(string argument, string paramName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if string argument is null or whitespace.
        /// </summary>
        public static void IsNotNullOrWhiteSpace(string argument, string paramName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
