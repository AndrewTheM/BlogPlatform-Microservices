namespace BlogPlatform.UI.Models;

public class PostAnalysisRequest
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string TagsString { get; set; }

    public string ImageString { get; set; }
}
