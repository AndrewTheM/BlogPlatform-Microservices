using Accounts.Domain.Enums;

namespace Accounts.Application.Common.DTO;

public class AccountDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string AvatarPath { get; set; }

    public Language? PreferredLanguage { get; set; }
}
