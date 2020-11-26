using System;

namespace Enable.Common
{
    /// <summary>
    /// Attribute that is appended to arguments when it has been verified
    /// that they are not null. This suppresses code analysis warning CA1062.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal class ValidatedNotNullAttribute : Attribute
    {
        // An implementation for the attribute is not needed, as it is the name
        // of the attribute which signals to the static analysis tool that it
        // has been verified the argument is not null.
    }
}
