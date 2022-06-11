using BlogPlatform.Shared.Common.Filters;
using FluentValidation;

namespace Comments.API.Validation;

public class CommentFilterValidator : AbstractValidator<CommentFilter>
{
    public CommentFilterValidator()
    {
        RuleFor(cf => cf.PageNumber)
            .GreaterThan(0);

        RuleFor(cf => cf.PageSize)
            .GreaterThan(0);

        RuleFor(cf => cf.Content)
            .Length(2, 50)
            .When(pf => pf.Content is not null);
    }
}
