using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace BlogPlatform.Comments.API.Validation;

public class CommentRequestValidator : AbstractValidator<CommentRequest>
{
    public CommentRequestValidator()
    {
        Include(new CommentContentRequestValidator());

        RuleFor(cr => cr.PostId)
            .NotEmpty();
        
        RuleFor(cr => cr.AuthorId)
            .NotEmpty();
    }
}
