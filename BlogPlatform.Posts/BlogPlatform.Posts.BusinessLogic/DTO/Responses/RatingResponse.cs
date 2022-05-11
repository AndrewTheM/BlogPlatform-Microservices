namespace BlogPlatform.Posts.BusinessLogic.DTO.Responses;

public class RatingResponse
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public string User { get; set; }

    public int RatingValue { get; set; }
}
