namespace Posts.BusinessLogic.DTO.Requests;

public class RatingRequest : RatingUpdateRequest
{
    public Guid PostId { get; set; }

    public Guid UserId { get; set; }
}
