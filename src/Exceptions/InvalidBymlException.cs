namespace CsOead.Exceptions;

public class InvalidBymlException : Exception
{
    public InvalidBymlException() { }
    public InvalidBymlException(string? message) : base(message) { }
    public InvalidBymlException(string? message, Exception? innerException) : base(message, innerException) { }
}
