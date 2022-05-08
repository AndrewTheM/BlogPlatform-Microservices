using BlogPlatform.DataAccess.Context;
using BlogPlatform.DataAccess.Entities;
using BlogPlatform.DataAccess.Repositories.Contracts;

namespace BlogPlatform.DataAccess.Repositories
{
    public class VerificationStatusRepository : EntityRepository<VerificationStatus, int>,
                                                IVerificationStatusRepository
    {
        public VerificationStatusRepository(BlogContext context)
            : base(context)
        {
        }
    }
}
