using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace BlogPlatform.Posts.API.Validation
{
    public class RatingRequestValidator : AbstractValidator<RatingRequest>
    {
        public RatingRequestValidator()
        {
            RuleFor(rr => rr.PostId).GreaterThan(0);
            RuleFor(rr => rr.RatingValue).InclusiveBetween(1, 5);
        }
    }
}
