using BlogPlatform.Accounts.Domain.Abstract;
using BlogPlatform.Accounts.Domain.Enums;

namespace BlogPlatform.Accounts.Domain.Entities;

public class ApplicationFeedback : EntityBase
{
    public ReviewResult Result { get; set; }

    public string ResponseText { get; set; }

    public AuthorApplication Application { get; set; }
}
