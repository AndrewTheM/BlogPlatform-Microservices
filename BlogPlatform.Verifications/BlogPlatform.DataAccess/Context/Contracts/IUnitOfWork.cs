using System;
using System.Threading.Tasks;

namespace BlogPlatform.DataAccess.Context.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
