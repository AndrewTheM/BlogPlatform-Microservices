using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Repositories.Contracts
{
    public interface ITagRepository<TId> : IRepository<Tag, TId>
    {
        Task<IEnumerable<Tag>> GetRelevantTagsAsync();

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no tag with given <paramref name="name"/> is found.
        /// </summary>
        Task<Tag> GetTagByNameAsync(string name);
    }

    public interface ITagRepository : ITagRepository<int>
    {
    }
}