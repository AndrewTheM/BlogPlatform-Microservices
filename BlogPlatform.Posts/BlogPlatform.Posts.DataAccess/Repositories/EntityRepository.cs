using BlogPlatform.Posts.DataAccess.Context;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Posts.DataAccess.Repositories;

public class EntityRepository<TEntity> : IRepository<TEntity>
    where TEntity : EntityBase
{
    protected readonly BlogContext _context;
    protected readonly DbSet<TEntity> _set;

    public EntityRepository(BlogContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public Task<IQueryable<TEntity>> GetAllAsync()
    {
        var records = _set.AsNoTracking();
        return Task.FromResult(records);
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await EnsureEntityResultAsync(() =>
        {
            return _set.FindAsync(id).AsTask();
        });
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        _set.Remove(entity);
    }

    /// <summary>
    /// Accepts a delegate that retrieves an entity asynchronously.
    /// Returns the execution result of the delegate.
    /// Throws <see cref="EntityNotFoundException" />
    /// if the result is null or the operation is invalid.
    /// </summary>
    protected async Task<TEntity> EnsureEntityResultAsync(Func<Task<TEntity>> entityDelegate)
    {
        try
        {
            return await entityDelegate()
                ?? throw new EntityNotFoundException();
        }
        catch (InvalidOperationException ex)
        {
            throw new EntityNotFoundException("An exception occured while retrieving the entity.", ex);
        }
    }
}
