using FluentValidation;

namespace BlogPlatform.Accounts.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(com => com.UserId)
            .NotEmpty();
    }
}
