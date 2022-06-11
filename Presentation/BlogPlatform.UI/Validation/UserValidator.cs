using BlogPlatform.UI.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BlogPlatform.UI.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IStringLocalizer<ValidationResource> locale)
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .Length(3, 50)
            .WithMessage(locale["UsernameInvalidLength"]);

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .EmailAddress()
            .WithMessage(locale["EmailInvalid"]);

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .MaximumLength(50)
            .WithMessage(locale["PasswordTooLong"]);

        RuleFor(u => u.ConfirmPassword)
            .NotEmpty()
            .WithMessage(locale["FieldEmpty"])
            .Equal(u => u.Password)
            .WithMessage(locale["PasswordNotConfirmed"]);
    }
}
