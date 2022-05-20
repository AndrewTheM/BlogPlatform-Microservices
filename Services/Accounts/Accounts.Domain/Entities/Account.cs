using Accounts.Domain.Abstract;
using Accounts.Domain.Enums;
using Accounts.Domain.ValueObjects;

namespace Accounts.Domain.Entities;

public class Account : EntityBase
{
    public Guid UserId { get; set; }

    public Name Name { get; set; }

    public Location Location { get; set; }

    public string AvatarPath { get; set; }

    public Language? PreferredLanguage { get; set; }
}
