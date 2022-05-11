using BlogPlatform.Posts.DataAccess.Filters;
using FluentValidation;

namespace BlogPlatform.Posts.API.Validation;

public class PostFilterValidator : AbstractValidator<PostFilter>
{
    public PostFilterValidator()
    {
        RuleFor(pf => pf.PageNumber)
            .GreaterThan(0);

        RuleFor(pf => pf.PageSize)
            .GreaterThan(0);

        RuleFor(pf => pf.Title)
            .Length(2, 50)
            .When(pf => pf.Title is not null);

        RuleFor(pf => pf.Author)
            .Length(3, 50)
            .When(pf => pf.Author is not null);

        RuleFor(pf => pf.Tag)
            .Length(2, 50)
            .When(pf => pf.Tag is not null);

        RuleFor(pf => pf.Year)
            .GreaterThanOrEqualTo(1970)
            .When(pf => pf.Year is not null);

        RuleFor(pf => pf.Month)
            .InclusiveBetween(1, 12)
            .When(pf => pf.Month is not null);

        RuleFor(pf => pf.Day)
            .InclusiveBetween(1, 31)
            .When(pf => pf.Day is not null);
    }
}
