using System;

namespace BlogPlatform.Comments.BusinessLogic.DTO.Responses
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public int UpvoteCount { get; set; }

        public DateTime PublishedOn { get; set; }

        public string RelativePublishTime { get; set; }

        public bool IsEdited { get; set; }
    }
}
