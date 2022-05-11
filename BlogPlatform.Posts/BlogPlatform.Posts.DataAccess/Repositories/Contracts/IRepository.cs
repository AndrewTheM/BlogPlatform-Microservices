using BlogPlatform.Posts.DataAccess.Extensions;

namespace BlogPlatform.Posts.DataAccess.Repositories.Contracts;

public interface IRepository<TEntity>
{
    Task<IQueryable<TEntity>> GetAllAsync();

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no entity with given <paramref name="id"/> is found.
    /// </summary>
    Task<TEntity> GetByIdAsync(Guid id);

    Task CreateAsync(TEntity entity);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no entity with given <paramref name="id"/> is found.
    /// </summary>
    Task DeleteAsync(Guid id);
}
