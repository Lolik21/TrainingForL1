using System;
using System.Runtime.Serialization;

namespace FileSystemVisitor.Exceptions
{
    [Serializable]
    public class VisitingAbortedException : Exception
    {
        public VisitingAbortedException()
        {
        }

        public VisitingAbortedException(string message) : base(message)
        {
        }

        public VisitingAbortedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VisitingAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}