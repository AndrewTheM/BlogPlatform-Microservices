using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BlogPlatform.Comments.API.Validation;

public class CommentVoteRequestValidator : AbstractValidator<CommentVoteRequest>
{
    public CommentVoteRequestValidator(IStringLocalizer<ValidationResource> locale)
    {
        RuleFor(cvr => cvr.VoteValue)
            .Must(BeSingle)
            .WithMessage(locale["NotSingleValue"]);
    }

    private bool BeSingle(int value) => value is 1 or -1;
}
