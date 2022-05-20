namespace Posts.DataAccess.Context.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}
