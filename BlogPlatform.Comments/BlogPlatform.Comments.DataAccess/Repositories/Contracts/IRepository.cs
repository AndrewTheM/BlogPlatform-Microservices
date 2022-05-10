namespace BlogPlatform.Comments.DataAccess.Repositories.Contracts;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetAsync(Guid id);

    Task<Guid> CreateAsync(T entity);

    Task UpdateAsync(Guid id, T newEntity);

    Task DeleteAsync(Guid id);
}
