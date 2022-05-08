using System.Collections.Generic;

namespace BlogPlatform.Posts.DataAccess.Entities
{
    public class Tag : EntityBase<int>
    {
        public string TagName { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
