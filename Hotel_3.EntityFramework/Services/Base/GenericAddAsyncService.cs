using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;

namespace Hotel_3.EntityFramework.Services.Base;

public class GenericAddAsyncService<T> : IAddAsyncService<T> where T : EntityObject
{
    public async Task<T?> AddAsync(T entity)
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }
}