namespace BlogPlatform.Posts.BusinessLogic.DTO.Responses
{
    public class RatingResponse
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string User { get; set; }

        public int RatingValue { get; set; }
    }
}
