using Accounts.Domain.Abstract;
using Accounts.Domain.Enums;

namespace Accounts.Domain.Entities;

public class ApplicationFeedback : EntityBase
{
    public ReviewResult Result { get; set; }

    public string ResponseText { get; set; }

    public AuthorApplication Application { get; set; }
}
