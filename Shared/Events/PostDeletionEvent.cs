namespace BlogPlatform.Shared.Events;

public class PostDeletionEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Datetime { get; set; } = DateTime.Now;

    public Guid PostId { get; set; }
}
