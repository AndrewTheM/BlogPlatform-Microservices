using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BlogPlatform.Posts.API.Validation;

public class PostTagsRequestValidator : AbstractValidator<PostTagsRequest>
{
    public PostTagsRequestValidator(IStringLocalizer<ValidationResource> locale)
    {
        RuleFor(ptr => ptr.Tags)
            .Must(tags => tags.Length <= 20)
            .When(p => p.Tags is not null)
            .WithMessage(locale["TagsLimit"]);

        RuleForEach(ptr => ptr.Tags)
            .NotEmpty()
            .WithMessage(locale["TagEmpty"])
            .Length(2, 50)
            .WithMessage(locale["TagInvalidLength"]);
    }
}
