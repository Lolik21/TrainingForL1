using System;
using System.Runtime.Serialization;

namespace ExceptionHandlingTask2
{
    [Serializable]
    public class ArgumentNullOrWhiteSpaceException : Exception
    {
        public ArgumentNullOrWhiteSpaceException()
        {
        }

        protected ArgumentNullOrWhiteSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ArgumentNullOrWhiteSpaceException(string message) : base(message)
        {
        }

        public ArgumentNullOrWhiteSpaceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}