namespace Posts.DataAccess.Entities;

public class Post : EntityBase
{
    public Guid AuthorId { get; set; }

    public string Title { get; set; }

    public string TitleIdentifier { get; set; }

    public string ThumbnailPath { get; set; }

    public PostContent ContentEntity { get; set; }

    public IList<Tag> Tags { get; set; }

    public IEnumerable<Rating> Ratings { get; set; }
}
