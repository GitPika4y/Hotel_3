namespace Hotel_3.Domain.Services.Base;

public interface IAddAsyncService<T>
{
    Task<T?> AddAsync(T entity);
}