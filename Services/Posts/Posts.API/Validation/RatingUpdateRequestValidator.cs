using FluentValidation;
using Posts.BusinessLogic.DTO.Requests;

namespace Posts.API.Validation;

public class RatingUpdateRequestValidator : AbstractValidator<RatingUpdateRequest>
{
    public RatingUpdateRequestValidator()
    {
        RuleFor(rr => rr.RatingValue)
            .InclusiveBetween(1, 5);
    }
}
