using System.Runtime.Serialization;

namespace KindleExportToMarkdown.Exceptions
{
    public class MalFormedHtmlException : Exception
    {
        public MalFormedHtmlException() : base() { }
        public MalFormedHtmlException(string message) : base(message) { }
        public MalFormedHtmlException(string message, Exception inner) : base(message, inner) { }
        protected MalFormedHtmlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
 