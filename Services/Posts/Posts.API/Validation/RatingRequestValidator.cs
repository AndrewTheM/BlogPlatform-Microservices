using FluentValidation;
using Posts.BusinessLogic.DTO.Requests;

namespace Posts.API.Validation;

public class RatingRequestValidator : AbstractValidator<RatingRequest>
{
    public RatingRequestValidator()
    {
        Include(new RatingUpdateRequestValidator());

        RuleFor(rr => rr.PostId)
            .NotEmpty();

        RuleFor(rr => rr.UserId)
            .NotEmpty();
    }
}
