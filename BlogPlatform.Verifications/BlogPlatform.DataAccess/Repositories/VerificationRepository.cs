using BlogPlatform.DataAccess.Context;
using BlogPlatform.DataAccess.Entities;
using BlogPlatform.DataAccess.Repositories.Contracts;

namespace BlogPlatform.DataAccess.Repositories
{
    public class VerificationRepository : EntityRepository<AuthorVerification, int>, IVerificationRepository
    {
        public VerificationRepository(BlogContext context)
            : base(context)
        {
        }
    }
}
