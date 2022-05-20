using Comments.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace Comments.API.Validation;

public class CommentContentRequestValidator : AbstractValidator<CommentContentRequest>
{
    public CommentContentRequestValidator()
    {
        RuleFor(cr => cr.Content)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
