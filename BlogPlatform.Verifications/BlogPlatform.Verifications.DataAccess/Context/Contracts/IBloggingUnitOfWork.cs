using BlogPlatform.Verifications.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Verifications.DataAccess.Context.Contracts;

public interface IBloggingUnitOfWork : IUnitOfWork
{
    IVerificationRepository AuthorVerifications { get; }

    IVerificationStatusRepository VerificationStatuses { get; }
}
