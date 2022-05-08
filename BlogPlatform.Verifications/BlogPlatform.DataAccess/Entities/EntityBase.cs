using System;

namespace BlogPlatform.DataAccess.Entities
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public DateTime UpdatedOn { get; set; }
    }
}
