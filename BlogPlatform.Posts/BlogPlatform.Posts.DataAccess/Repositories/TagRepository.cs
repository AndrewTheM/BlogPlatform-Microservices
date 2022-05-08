using BlogPlatform.Posts.DataAccess.Context;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Repositories
{
    public class TagRepository : EntityRepository<Tag, int>, ITagRepository
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
}
