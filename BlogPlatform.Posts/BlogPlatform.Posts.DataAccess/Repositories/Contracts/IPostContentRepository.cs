using BlogPlatform.Posts.DataAccess.Entities;

namespace BlogPlatform.Posts.DataAccess.Repositories.Contracts
{
    public interface IPostContentRepository<TId> : IRepository<PostContent, TId>
    {
    }

    public interface IPostContentRepository : IPostContentRepository<int>
    {
    }
}
