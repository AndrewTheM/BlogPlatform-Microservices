using BlogPlatform.DataAccess.Repositories.Contracts;

namespace BlogPlatform.DataAccess.Context.Contracts
{
    public interface IBloggingUnitOfWork : IUnitOfWork
    {
        IVerificationRepository AuthorVerifications { get; }

        IVerificationStatusRepository VerificationStatuses { get; }
    }
}
