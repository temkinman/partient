namespace Hospital.Application.Interfaces;

public interface IBaseItemRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(T item, CancellationToken cancellationToken = default);
    Task<bool> RemoveAllItemsAsync(CancellationToken cancellationToken = default);
}
