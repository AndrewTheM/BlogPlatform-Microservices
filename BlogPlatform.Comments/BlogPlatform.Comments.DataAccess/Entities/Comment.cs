namespace BlogPlatform.Comments.DataAccess.Entities
{
    public class Comment : EntityBase<int>
    {
        public int PostId { get; set; }

        public string AuthorId { get; set; }

        public string Content { get; set; }

        public int UpvoteCount { get; set; }
    }
}
