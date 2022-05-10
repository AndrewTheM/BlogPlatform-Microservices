namespace BlogPlatform.Comments.DataAccess.Entities;

public class Comment
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public Guid AuthorId { get; set; }

    public string Content { get; set; }

    public int UpvoteCount { get; set; }

    public DateTime CreatedOn { get; init; }

    public DateTime UpdatedOn { get; set; }
}
