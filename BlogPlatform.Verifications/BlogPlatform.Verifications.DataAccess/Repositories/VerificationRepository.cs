using BlogPlatform.Verifications.DataAccess.Context;
using BlogPlatform.Verifications.DataAccess.Entities;
using BlogPlatform.Verifications.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Verifications.DataAccess.Repositories;

public class VerificationRepository : EntityRepository<AuthorVerification>, IVerificationRepository
{
    public VerificationRepository(BlogContext context)
        : base(context)
    {
    }
}
