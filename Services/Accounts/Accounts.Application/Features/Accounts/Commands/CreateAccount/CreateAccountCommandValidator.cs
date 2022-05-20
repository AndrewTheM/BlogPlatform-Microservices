using FluentValidation;

namespace Accounts.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(com => com.UserId)
            .NotEmpty();
    }
}
