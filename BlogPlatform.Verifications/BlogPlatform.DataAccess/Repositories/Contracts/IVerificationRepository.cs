using BlogPlatform.DataAccess.Entities;

namespace BlogPlatform.DataAccess.Repositories.Contracts
{
    public interface IVerificationRepository<TId> : IRepository<AuthorVerification, TId>
    {
    }

    public interface IVerificationRepository : IVerificationRepository<int>
    {
    }
}
