using Accounts.Domain.Abstract;
using Accounts.Domain.ValueObjects;

namespace Accounts.Domain.Entities;

public class AuthorApplication : EntityBase
{
    public Guid UserId { get; set; }

    public Name FullName { get; set; }

    public string ContactEmail { get; set; }

    public string Annotation { get; set; }

    public Guid? FeedbackId { get; set; }
    public ApplicationFeedback Feedback { get; set; }
}
