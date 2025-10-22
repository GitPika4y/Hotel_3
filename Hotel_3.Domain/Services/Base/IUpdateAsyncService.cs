namespace Hotel_3.Domain.Services.Base;

public interface IUpdateAsyncService<T>
{
        Task<T?> UpdateAsync(T entity);
    
}