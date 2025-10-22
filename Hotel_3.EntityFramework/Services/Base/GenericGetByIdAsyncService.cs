using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace Hotel_3.EntityFramework.Services.Base;

public class GenericGetByIdAsyncService<T> : IGetByIdAsyncService<T> where T : EntityObject 
{
    public async Task<T?> GetByIdAsync(int id)
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        return entity;
    }
}