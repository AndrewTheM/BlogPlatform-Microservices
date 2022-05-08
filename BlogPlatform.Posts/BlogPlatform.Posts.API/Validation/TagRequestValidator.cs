using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using FluentValidation;

namespace BlogPlatform.Posts.API.Validation
{
    public class TagRequestValidator : AbstractValidator<TagRequest>
    {
        public TagRequestValidator()
        {
            RuleFor(tr => tr.TagName).NotEmpty()
                                     .Length(2, 50);
        }
    }
}
