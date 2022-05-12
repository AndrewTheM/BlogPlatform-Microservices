using BlogPlatform.Verifications.Domain.Abstract;
using BlogPlatform.Verifications.Domain.ValueObjects;

namespace BlogPlatform.Verifications.Domain.Entities;

public class AuthorApplication : EntityBase
{
    public Guid UserId { get; set; }

    public Name FullName { get; set; } 

    public string ContactEmail { get; set; }

    public string Annotation { get; set; }

    public Guid? FeedbackId { get; set; }
    public ApplicationFeedback Feedback { get; set; }
}
