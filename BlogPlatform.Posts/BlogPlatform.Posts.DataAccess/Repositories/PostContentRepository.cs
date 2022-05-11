using BlogPlatform.Posts.DataAccess.Context;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Posts.DataAccess.Repositories;

public class PostContentRepository : EntityRepository<PostContent>, IPostContentRepository
{
    public PostContentRepository(BlogContext context)
        : base(context)
    {
    }
}
