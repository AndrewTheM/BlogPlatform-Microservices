namespace BlogPlatform.UI.Models;

public class Rating
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public string User { get; set; }

    public int RatingValue { get; set; }
}
