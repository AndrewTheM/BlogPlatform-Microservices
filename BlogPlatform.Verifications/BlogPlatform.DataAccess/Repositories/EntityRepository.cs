using BlogPlatform.DataAccess.Context;
using BlogPlatform.DataAccess.Entities;
using BlogPlatform.DataAccess.Extensions;
using BlogPlatform.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.DataAccess.Repositories
{
    public class EntityRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : EntityBase<TId>
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

        public async Task<TEntity> GetByIdAsync(TId id)
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

        public async Task DeleteAsync(TId id)
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
}
