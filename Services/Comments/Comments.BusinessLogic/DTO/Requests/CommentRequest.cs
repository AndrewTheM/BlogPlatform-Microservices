namespace Comments.BusinessLogic.DTO.Requests;

public class CommentRequest : CommentContentRequest
{
    public Guid PostId { get; set; }

    public Guid AuthorId { get; set; }
}
