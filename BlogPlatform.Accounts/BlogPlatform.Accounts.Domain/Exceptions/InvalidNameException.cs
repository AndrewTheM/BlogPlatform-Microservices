namespace BlogPlatform.Accounts.Domain.Exceptions;

public class InvalidNameException : Exception
{
    private const string DefaultMessage = "The name is not valid.";

    public InvalidNameException()
        : base(DefaultMessage)
    {
    }

    public InvalidNameException(string message)
        : base(message)
    {
    }

    public InvalidNameException(Exception innerException)
        : base(DefaultMessage, innerException)
    {
    }

    public InvalidNameException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
