namespace Accounts.Domain.Abstract;

public abstract class EntityBase
{
    public Guid Id { get; init; }

    public DateTime CreatedOn { get; init; }

    public DateTime UpdatedOn { get; set; }
}
