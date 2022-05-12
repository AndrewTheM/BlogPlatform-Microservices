using FluentValidation;

namespace BlogPlatform.Accounts.Application.Features.Accounts.Commands.EditAccount;

public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
{
    public EditAccountCommandValidator()
    {
        RuleFor(com => com.Id)
            .NotEmpty();

        RuleFor(com => com.FirstName)
            .Length(1, 50)
            .Unless(com => string.IsNullOrEmpty(com.FirstName));
        
        RuleFor(com => com.MiddleName)
            .Length(1, 50)
            .Unless(com => string.IsNullOrEmpty(com.MiddleName));
        
        RuleFor(com => com.LastName)
            .Length(1, 50)
            .Unless(com => string.IsNullOrEmpty(com.LastName));

        RuleFor(com => com.City)
            .Length(1, 50)
            .Unless(com => string.IsNullOrEmpty(com.City));
        
        RuleFor(com => com.Country)
            .Length(1, 50)
            .Unless(com => string.IsNullOrEmpty(com.Country));
        
        RuleFor(com => com.State)
            .Length(1, 50)
            .Unless(com => string.IsNullOrEmpty(com.State));

        RuleFor(com => com.AvatarPath)
            .Length(5, 500)
            .Unless(com => string.IsNullOrEmpty(com.AvatarPath));

        RuleFor(com => com.PreferredLanguage)
            .IsInEnum()
            .Unless(com => com.PreferredLanguage is null);
    }
}
