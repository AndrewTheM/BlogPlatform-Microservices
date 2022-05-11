using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace BlogPlatform.Posts.API.Validation;

public class RatingUpdateRequestValidator : AbstractValidator<RatingUpdateRequest>
{
    public RatingUpdateRequestValidator()
    {
        RuleFor(rr => rr.RatingValue)
            .InclusiveBetween(1, 5);
    }
}
