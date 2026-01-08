namespace ATS.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);

    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);

    Task<int> CountTotalAsync();

    Task<T> UpdateAsync(T entity);

    Task<bool> DeleteAsync(Guid id);
}
