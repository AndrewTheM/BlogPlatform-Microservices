using Posts.DataAccess.Context;
using Posts.DataAccess.Entities;
using Posts.DataAccess.Repositories.Contracts;

namespace Posts.DataAccess.Repositories;

public class PostContentRepository : EntityRepository<PostContent>, IPostContentRepository
{
    public PostContentRepository(BlogContext context)
        : base(context)
    {
    }
}
