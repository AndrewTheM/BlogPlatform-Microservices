using BlogPlatform.Accounts.Domain.Enums;
using MediatR;

namespace BlogPlatform.Accounts.Application.Features.Accounts.Commands.EditAccount;

public class EditAccountCommand : IRequest
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
    
    public string City { get; set; }

    public string State { get; set; }
    
    public string Country { get; set; }

    public string AvatarPath { get; set; }

    public Language? PreferredLanguage { get; set; }
}
