using System;

namespace BlogPlatform.Posts.BusinessLogic.DTO.Responses
{
    public class PostResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleIdentifier { get; set; }

        public string Author { get; set; }

        public DateTime PublishedOn { get; set; }

        public string RelativePublishTime { get; set; }

        public bool IsEdited { get; set; }

        public string ThumbnailPath { get; set; }
    }
}
