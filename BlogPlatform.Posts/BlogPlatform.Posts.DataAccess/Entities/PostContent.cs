namespace BlogPlatform.Posts.DataAccess.Entities
{
    public class PostContent : EntityBase<int>
    {
        public string Content { get; set; }

        public Post Post { get; set; }
    }
}
