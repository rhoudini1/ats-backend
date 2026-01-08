namespace ATS.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<int> CountTotalAsync(CancellationToken cancellationToken);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
