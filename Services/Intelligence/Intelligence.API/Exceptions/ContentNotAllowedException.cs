namespace Intelligence.API.Exceptions;

public class ContentNotAllowedException : Exception
{
    private const string MessageTemplate = "{0} did not pass moderation. Reason: {1}.";

    public ContentNotAllowedException(string sourceElement, string reason)
        : base(string.Format(MessageTemplate, sourceElement, reason))
    {
        Reason = reason;
        SourceElement = sourceElement;
    }

    public string Reason { get; set; }

    public string SourceElement { get; set; }
}
