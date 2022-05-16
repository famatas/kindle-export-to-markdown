using System.Runtime.Serialization;

namespace KindleExportToMarkdown.Exceptions
{
    [Serializable]
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException() : base() { }
        public InvalidFileTypeException(string message): base(message) { }
        public InvalidFileTypeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidFileTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

