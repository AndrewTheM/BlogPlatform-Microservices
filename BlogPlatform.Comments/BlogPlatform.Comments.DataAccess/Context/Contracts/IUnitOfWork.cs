using System;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.DataAccess.Context.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
