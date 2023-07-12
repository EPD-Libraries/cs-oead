using System.Runtime.Serialization;

namespace CsOead.Exceptions;

public class InvalidSarcException : Exception
{
    public InvalidSarcException() { }
    public InvalidSarcException(string? message) : base(message) { }
    public InvalidSarcException(string? message, Exception? innerException) : base(message, innerException) { }
    protected InvalidSarcException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
