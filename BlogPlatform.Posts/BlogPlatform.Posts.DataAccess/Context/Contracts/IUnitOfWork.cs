using System;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Context.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
