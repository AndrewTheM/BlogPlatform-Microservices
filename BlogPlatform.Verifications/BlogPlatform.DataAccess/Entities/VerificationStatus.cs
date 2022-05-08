using System.Collections.Generic;

namespace BlogPlatform.DataAccess.Entities
{
    public class VerificationStatus : EntityBase<int>
    {
        public string StatusName { get; set; }

        public IEnumerable<AuthorVerification> Verifications { get; set; }
    }
}
