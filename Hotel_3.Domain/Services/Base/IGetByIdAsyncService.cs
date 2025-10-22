namespace Hotel_3.Domain.Services.Base;

public interface IGetByIdAsyncService<T>
{
    Task<T?> GetByIdAsync(int id);
}