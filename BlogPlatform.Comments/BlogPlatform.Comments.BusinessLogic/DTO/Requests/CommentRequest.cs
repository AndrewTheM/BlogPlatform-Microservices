namespace BlogPlatform.Comments.BusinessLogic.DTO.Requests;

public class CommentRequest
{
    public Guid PostId { get; set; }

    public string Content { get; set; }
}
