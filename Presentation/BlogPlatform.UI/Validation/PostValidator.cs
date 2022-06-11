using BlogPlatform.UI.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BlogPlatform.UI.Validation;

public class PostValidator : AbstractValidator<Post>
{
    public PostValidator(IStringLocalizer<ValidationResource> locale)
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .Length(3, 200)
            .WithMessage(locale["TitleInvalidLength"]);

        RuleFor(p => p.Content)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .MaximumLength(20000)
            .WithMessage(locale["ContentTooLong"]);

        RuleFor(p => p.Tags)
            .Must(tags => tags.Count <= 20)
            .When(p => p.Tags is not null)
            .WithMessage(locale["TagsLimit"])
            //.Must(tags => tags.All(t => !string.IsNullOrWhiteSpace(t)))
            //.WithMessage(locale["TagEmpty"])
            .Must(tags => tags.All(t => t.Length is >= 2 and <= 50))
            .WithMessage(locale["TagInvalidLength"]);

        //RuleForEach(p => p.Tags)
        //    .NotEmpty()
        //    .WithMessage(locale["TagEmpty"])
        //    .Length(2, 50)
        //    .WithMessage(locale["TagInvalidLength"]);
    }
}
