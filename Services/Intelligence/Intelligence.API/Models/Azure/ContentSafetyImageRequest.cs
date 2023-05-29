namespace Intelligence.API.Models.Azure;

internal class ContentSafetyImageRequest
{
    public ContentSafetyImageRequest(string content)
    {
        Image = new() { Content = content };
    }

    // Base 64 string
    public ContentRequest Image { get; set; }
}
