using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class RoomService : IRoomService
{
    private readonly GenericAddAsyncService<Room> _adder = new();
    private readonly GenericUpdateAsyncService<Room> _updater = new();
    private readonly GenericGetAllAsyncService<Room> _getterAll = new();


    public async Task<Room?> AddAsync(Room entity)
    {
        return await _adder.AddAsync(entity);
    }

    public async Task<Room?> UpdateAsync(Room entity)
    {
        return await _updater.UpdateAsync(entity);
    }

    public Task<IEnumerable<Room>> GetAllAsync(params Expression<Func<Room, object>>[] includes)
    {
        return  _getterAll.GetAllAsync(includes);
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _getterAll.GetAllAsync();
    }
}