using BlogPlatform.DataAccess.Entities;

namespace BlogPlatform.DataAccess.Repositories.Contracts
{
    public interface IVerificationStatusRepository<TId> : IRepository<VerificationStatus, TId>
    {
    }

    public interface IVerificationStatusRepository : IVerificationStatusRepository<int>
    {
    }
}
