namespace BlogPlatform.Verifications.DataAccess.Context.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}
