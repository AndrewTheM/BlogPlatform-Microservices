using BlogPlatform.Verifications.DataAccess.Context;
using BlogPlatform.Verifications.DataAccess.Entities;
using BlogPlatform.Verifications.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Verifications.DataAccess.Repositories;

public class VerificationStatusRepository : EntityRepository<VerificationStatus>, IVerificationStatusRepository
{
    public VerificationStatusRepository(BlogContext context)
        : base(context)
    {
    }
}
