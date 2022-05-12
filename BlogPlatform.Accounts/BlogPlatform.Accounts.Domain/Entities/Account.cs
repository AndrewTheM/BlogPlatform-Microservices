using BlogPlatform.Verifications.Domain.Abstract;
using BlogPlatform.Verifications.Domain.Enums;
using BlogPlatform.Verifications.Domain.ValueObjects;

namespace BlogPlatform.Verifications.Domain.Entities;

public class Account : EntityBase
{
    public Guid UserId { get; set; }

    public Name Name { get; set; }

    public Location Location { get; set; }

    public string AvatarPath { get; set; }

    public Language? PreferredLanguage { get; set; }
}
