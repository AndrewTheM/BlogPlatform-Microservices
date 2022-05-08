using BlogPlatform.DataAccess.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.DataAccess.Repositories.Contracts
{
    public interface IRepository<TEntity, TId>
    {
        Task<IQueryable<TEntity>> GetAllAsync();

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no entity with given <paramref name="id"/> is found.
        /// </summary>
        Task<TEntity> GetByIdAsync(TId id);

        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Throws <see cref="EntityNotFoundException"/>
        /// if no entity with given <paramref name="id"/> is found.
        /// </summary>
        Task DeleteAsync(TId id);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
    {
    }
}
