using System;

namespace BlogPlatform.Posts.DataAccess.Entities
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public DateTime UpdatedOn { get; set; }
    }
}
