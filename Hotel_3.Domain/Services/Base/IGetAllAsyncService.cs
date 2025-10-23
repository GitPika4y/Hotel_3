using System.Linq.Expressions;

namespace Hotel_3.Domain.Services.Base;

public interface IGetAllAsyncService<T>
{
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync();
}