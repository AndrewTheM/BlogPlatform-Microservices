using FluentValidation;
using Posts.BusinessLogic.DTO.Requests;

namespace Posts.API.Validation;

public class TagRequestValidator : AbstractValidator<TagRequest>
{
    public TagRequestValidator()
    {
        RuleFor(tr => tr.TagName)
            .NotEmpty()
            .Length(2, 50);
    }
}
