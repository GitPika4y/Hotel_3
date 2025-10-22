using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;

namespace Hotel_3.EntityFramework.Services.Base;

public class GenericUpdateAsyncService<T> : IUpdateAsyncService<T> where T: EntityObject
{
    public async Task<T?> UpdateAsync(T entity)
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }
}