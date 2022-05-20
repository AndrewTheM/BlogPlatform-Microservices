namespace Comments.BusinessLogic.DTO.Responses;

public class CommentResponse
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public string Author { get; set; }

    public string Content { get; set; }

    public int UpvoteCount { get; set; }

    public DateTime PublishedOn { get; set; }

    public string RelativePublishTime { get; set; }

    public bool IsEdited { get; set; }
}
