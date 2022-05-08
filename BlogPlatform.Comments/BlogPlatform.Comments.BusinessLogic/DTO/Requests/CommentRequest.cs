namespace BlogPlatform.Comments.BusinessLogic.DTO.Requests
{
    public class CommentRequest
    {
        public int PostId { get; set; }

        public string Content { get; set; }
    }
}
