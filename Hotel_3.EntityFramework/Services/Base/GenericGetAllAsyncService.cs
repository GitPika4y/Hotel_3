using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace Hotel_3.EntityFramework.Services.Base;

public class GenericGetAllAsyncService<T> : IGetAllAsyncService<T> where T : EntityObject
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        return await context.Set<T>().ToListAsync();
    }
}