namespace Hotel_3.Domain.Services.Base;

public interface IGetAllAsyncService<T>
{
    Task<IEnumerable<T>> GetAllAsync();
}