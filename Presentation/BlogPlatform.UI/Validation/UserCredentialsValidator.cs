using BlogPlatform.UI.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BlogPlatform.UI.Validation;

public class UserCredentialsValidator : AbstractValidator<UserCredentials>
{
    public UserCredentialsValidator(IStringLocalizer<ValidationResource> locale)
    {
        RuleFor(uc => uc.Username)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .Length(3, 50)
            .WithMessage(locale["UsernameInvalidLength"]);

        RuleFor(uc => uc.Password)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .MaximumLength(50)
            .WithMessage(locale["PasswordTooLong"]);
    }
}
