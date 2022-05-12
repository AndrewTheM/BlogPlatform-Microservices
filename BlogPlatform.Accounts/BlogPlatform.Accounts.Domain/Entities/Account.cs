using BlogPlatform.Accounts.Domain.Abstract;
using BlogPlatform.Accounts.Domain.Enums;
using BlogPlatform.Accounts.Domain.ValueObjects;

namespace BlogPlatform.Accounts.Domain.Entities;

public class Account : EntityBase
{
    public Guid UserId { get; set; }

    public Name Name { get; set; }

    public Location Location { get; set; }

    public string AvatarPath { get; set; }

    public Language? PreferredLanguage { get; set; }
}
