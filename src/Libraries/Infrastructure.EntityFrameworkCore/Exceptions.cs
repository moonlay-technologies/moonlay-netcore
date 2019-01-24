using System;

namespace Core.Domain
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message)
            : base(message)
        { }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class DomainNullException : ArgumentNullException
    {
        public DomainNullException(string paramName) : base(paramName)
        {
        }

        public DomainNullException(string paramName, string message) : base(message, paramName)
        {
        }
    }
}