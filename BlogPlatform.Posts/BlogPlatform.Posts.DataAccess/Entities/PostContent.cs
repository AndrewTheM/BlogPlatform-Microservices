namespace BlogPlatform.Posts.DataAccess.Entities;

public class PostContent : EntityBase
{
    public string Content { get; set; }

    public Post Post { get; set; }
}
