using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace Hotel_3.EntityFramework.Services.Base;

public class GenericGetAllIncludeAsyncService<T> : IGetAllIncludeAsyncService<T> where T : EntityObject
{
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        var entities = await query.ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        query = includes.Aggregate(query, (current, path) => current.Include(path));

        return await query.ToListAsync(); 
    }
}