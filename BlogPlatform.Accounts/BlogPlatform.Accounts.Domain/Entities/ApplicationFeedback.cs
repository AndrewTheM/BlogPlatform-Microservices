using BlogPlatform.Verifications.Domain.Abstract;
using BlogPlatform.Verifications.Domain.Enums;

namespace BlogPlatform.Verifications.Domain.Entities;

public class ApplicationFeedback : EntityBase
{
    public ReviewResult Result { get; set; } 

    public string ResponseText { get; set; }

    public AuthorApplication Application { get; set; }
}
