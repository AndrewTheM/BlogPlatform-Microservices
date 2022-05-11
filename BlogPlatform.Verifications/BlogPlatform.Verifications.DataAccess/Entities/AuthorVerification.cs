namespace BlogPlatform.Verifications.DataAccess.Entities;

public class AuthorVerification : EntityBase
{
    public Guid UserId { get; set; }

    public string PromptText { get; set; }

    public string Response { get; set; }

    public Guid? VerificationStatusId { get; set; }
    public VerificationStatus VerificationStatus { get; set; }
}
