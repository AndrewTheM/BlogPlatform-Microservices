using Posts.DataAccess.Entities;
using Posts.DataAccess.Extensions;

namespace Posts.DataAccess.Repositories.Contracts;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetRelevantTagsAsync();

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no tag with given <paramref name="name"/> is found.
    /// </summary>
    Task<Tag> GetTagByNameAsync(string name);
}
