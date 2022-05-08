namespace BlogPlatform.Posts.DataAccess.Entities
{
    public class Rating : EntityBase<int>
    {
        public string UserId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int RatingValue { get; set; }
    }
}
