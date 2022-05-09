using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace BlogPlatform.Comments.API.Validation;

public class CommentRequestValidator : AbstractValidator<CommentRequest>
{
    public CommentRequestValidator()
    {
        RuleFor(cr => cr.PostId)
            .NotEmpty();

        RuleFor(cr => cr.Content)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
