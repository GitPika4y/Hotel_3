using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class RoomService : IRoomService
{
    private readonly GenericAddAsyncService<Room> _adder = new();
    private readonly GenericUpdateAsyncService<Room> _updater = new();
    private readonly GenericGetAllIncludeAsyncService<Room> _getterAll = new();
    private readonly GenericGetByIdAsyncService<Room> _getById = new();


    public async Task<Room?> AddAsync(Room entity)
    {
        return await _adder.AddAsync(entity);
    }

    public async Task<Room?> UpdateAsync(Room entity)
    {
        return await _updater.UpdateAsync(entity);
    }

    public async Task<IEnumerable<Room>> GetAllAsync(params Expression<Func<Room, object>>[] includes)
    {
        return await _getterAll.GetAllAsync(includes);
    }

    public async Task<IEnumerable<Room>> GetAllAsync(params string[] includes)
    {
        return await _getterAll.GetAllAsync(includes);
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _getById.GetByIdAsync(id);
    }
}