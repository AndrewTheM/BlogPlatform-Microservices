using System.Collections.Generic;

namespace BlogPlatform.Posts.DataAccess.Entities
{
    public class Post : EntityBase<int>
    {
        public string AuthorId { get; set; }

        public string Title { get; set; }

        public string TitleIdentifier { get; set; }

        public string ThumbnailPath { get; set; }

        public PostContent ContentEntity { get; set; }

        public IList<Tag> Tags { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }
    }
}
