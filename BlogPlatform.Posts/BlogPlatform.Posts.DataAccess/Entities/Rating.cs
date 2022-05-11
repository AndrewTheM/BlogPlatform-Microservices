namespace BlogPlatform.Posts.DataAccess.Entities;

public class Rating : EntityBase
{
    public Guid UserId { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; }

    public int RatingValue { get; set; }
}
