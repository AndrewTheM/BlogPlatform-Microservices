namespace Intelligence.API.Models.Azure;

internal class ContentSafetyTextRequest
{
    public ContentSafetyTextRequest(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}
