using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Context;
using Posts.DataAccess.Entities;
using Posts.DataAccess.Repositories.Contracts;

namespace Posts.DataAccess.Repositories;

public class TagRepository : EntityRepository<Tag>, ITagRepository
{
    public TagRepository(BlogContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Tag>> GetRelevantTagsAsync()
    {
        var tags = await _set.Include(t => t.Posts).ToListAsync();
        return tags.Where(t => t.Posts != null && t.Posts.Any());
    }

    public async Task<Tag> GetTagByNameAsync(string name)
    {
        return await EnsureEntityResultAsync(() =>
        {
            return _set.SingleAsync(t => t.TagName == name);
        });
    }
}
