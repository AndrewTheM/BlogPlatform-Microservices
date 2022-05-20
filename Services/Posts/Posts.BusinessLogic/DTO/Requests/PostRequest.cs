namespace Posts.BusinessLogic.DTO.Requests;

public class PostRequest
{
    public Guid AuthorId { get; set; }

    public string Title { get; set; }

    public string ThumbnailPath { get; set; }

    public string Content { get; set; }
}
