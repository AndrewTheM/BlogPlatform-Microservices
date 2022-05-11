namespace BlogPlatform.Posts.DataAccess.Entities;

public class Tag : EntityBase
{
    public string TagName { get; set; }

    public IEnumerable<Post> Posts { get; set; }
}
