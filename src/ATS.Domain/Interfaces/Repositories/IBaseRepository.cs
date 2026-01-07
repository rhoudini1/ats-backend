namespace ATS.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);

    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> UpdateAsync(T entity);

    Task<bool> DeleteAsync(Guid id);
}
