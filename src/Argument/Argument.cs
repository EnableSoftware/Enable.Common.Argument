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
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter, used in exception message if value is not defined in Enum.</param>
        public static void IsEnumDefined(Enum value, string paramName)
        {
            if (value == null)
            {
                return;
            }

            var type = value.GetType();

            if (!Enum.IsDefined(type, value))
            {
                throw new ArgumentOutOfRangeException(paramName, $"Specified argument was out of the range of valid values for enum '{type.Name}'.");
            }
        }

        /// <summary>
        /// Throws an exception if range predicate evaluates to false.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">The name of the parameter, used in exception message if condition is false.</param>
        public static void IsInRange(bool condition, string paramName)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if argument is null.
        /// </summary>
        /// <param name="argument">The object to check.</param>
        /// <param name="paramName">The name of the parameter, used in exception message if argument is null.</param>
        public static void IsNotNull(object argument, string paramName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if string argument is null or empty.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="paramName">The name of the parameter, used in exception message if argument is null or empty.</param>
        public static void IsNotNullOrEmpty(string argument, string paramName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (argument.Length == 0)
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }
        }

        /// <summary>
        /// Throws an exception if string argument is null or whitespace.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="paramName">The name of the parameter, used in exception message if argument is null or white space.</param>
        public static void IsNotNullOrWhiteSpace(string argument, string paramName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (argument.Length == 0)
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }

            for (int i = 0; i < argument.Length; i++)
            {
                if (!char.IsWhiteSpace(argument[i]))
                {
                    return;
                }
            }

            throw new ArgumentException("Value cannot be white space.", paramName);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings

        /// <summary>
        /// Throws an exception if the string argument is not a valid URI.
        /// </summary>
        /// <param name="uri">The string to check.</param>
        /// <param name="paramName">The name of the parameter, used in exception message if argument is not a well formed URI string.</param>
        /// <param name="kind">The kind of URI to check, defaults to Absolute.</param>
        public static void IsWellFormedUriString(string uri, string paramName, UriKind kind = UriKind.Absolute)
        {
            if (!Uri.IsWellFormedUriString(uri, kind))
            {
                string message;

                switch (kind)
                {
                    case UriKind.Absolute:
                        message = "Specified argument must be a valid absolute URI.";
                        break;

                    case UriKind.Relative:
                        message = "Specified argument must be a valid relative URI.";
                        break;

                    case UriKind.RelativeOrAbsolute:
                    default:
                        message = "Specified argument must be a valid URI.";
                        break;
                }

                throw new ArgumentException(message, paramName);
            }
        }

#pragma warning restore CA1054 // Uri parameters should not be strings
    }
}
