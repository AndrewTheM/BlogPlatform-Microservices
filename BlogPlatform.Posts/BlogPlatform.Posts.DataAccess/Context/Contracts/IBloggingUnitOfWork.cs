using BlogPlatform.Posts.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Posts.DataAccess.Context.Contracts
{
    public interface IBloggingUnitOfWork : IUnitOfWork
    {
        IPostRepository Posts { get; }

        IPostContentRepository PostContents { get; }

        ITagRepository Tags { get; }

        IRatingRepository Ratings { get; }
    }
}
