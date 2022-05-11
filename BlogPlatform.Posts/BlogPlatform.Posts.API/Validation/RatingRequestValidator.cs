using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace BlogPlatform.Posts.API.Validation;

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
