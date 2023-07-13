namespace CsOead.Exceptions;

public class InvalidSarcException : Exception
{
    public InvalidSarcException() { }
    public InvalidSarcException(string? message) : base(message) { }
    public InvalidSarcException(string? message, Exception? innerException) : base(message, innerException) { }
}
