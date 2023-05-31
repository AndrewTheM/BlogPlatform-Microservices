namespace BlogPlatform.UI.Models;

public class Post
{
    public Guid Id { get; set; }

    public string ThumbnailPath { get; set; }

    public string AudioPath { get; set; }

    public string TitleIdentifier { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Content { get; set; }

    public DateTime PublishedOn { get; set; }

    public string RelativePublishTime { get; set; }

    public bool IsEdited { get; set; }

    public double Rating { get; set; }

    public ISet<string> Tags { get; set; }

    public Page<Comment> CommentPage { get; set; }
}
