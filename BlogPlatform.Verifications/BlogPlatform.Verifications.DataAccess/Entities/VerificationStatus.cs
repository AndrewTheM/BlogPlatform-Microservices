namespace BlogPlatform.Verifications.DataAccess.Entities;

public class VerificationStatus : EntityBase
{
    public string StatusName { get; set; }

    public IEnumerable<AuthorVerification> Verifications { get; set; }
}
