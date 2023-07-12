namespace CsOead.Exceptions;

public class Yaz0Exception : Exception
{
    public Yaz0Exception() { }
    public Yaz0Exception(string? message) : base(message) { }
    public Yaz0Exception(string? message, Exception? innerException) : base(message, innerException) { }
}
