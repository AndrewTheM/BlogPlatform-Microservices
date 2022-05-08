namespace BlogPlatform.DataAccess.Entities
{
    public class AuthorVerification : EntityBase<int>
    {
        public string UserId { get; set; }

        public string PromptText { get; set; }

        public string Response { get; set; }

        public int? VerificationStatusId { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
    }
}
