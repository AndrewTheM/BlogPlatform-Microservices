using BlogPlatform.Posts.DataAccess.Context.Contracts;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Posts.DataAccess.Context;

public class UnitOfWork : IBloggingUnitOfWork
{
    private bool _isDisposed;

    private readonly BlogContext _context;

    public IPostRepository Posts { get; }

    public IPostContentRepository PostContents { get; }

    public ITagRepository Tags { get; }

    public IRatingRepository Ratings { get; }

    public UnitOfWork(
        BlogContext context,
        IPostRepository posts,
        IPostContentRepository postContents,
        ITagRepository tags,
        IRatingRepository ratings)
    {
        _context = context;
        Posts = posts;
        PostContents = postContents;
        Tags = tags;
        Ratings = ratings;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _isDisposed = true;
        }
    }
}
