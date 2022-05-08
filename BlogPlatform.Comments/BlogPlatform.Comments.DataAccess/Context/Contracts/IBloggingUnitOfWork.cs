using BlogPlatform.Comments.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Comments.DataAccess.Context.Contracts
{
    public interface IBloggingUnitOfWork : IUnitOfWork
    {
        ICommentRepository Comments { get; }
    }
}
