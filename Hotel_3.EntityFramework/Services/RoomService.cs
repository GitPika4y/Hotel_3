using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;
using Hotel_3.Domain.Services.Room;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class RoomService : IRoomService
{
    private readonly GenericAddAsyncService<Room> _adder = new();
    private readonly GenericUpdateAsyncService<Room> _updater = new();
    private readonly GenericGetByIdAsyncService<Room> _getterById = new();
    private readonly GenericGetAllAsyncService<Room> _getterAll = new();


    public async Task<Room?> AddAsync(Room entity)
    {
        return await _adder.AddAsync(entity);
    }

    public async Task<Room?> UpdateAsync(Room entity)
    {
        return await _updater.UpdateAsync(entity);
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _getterById.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _getterAll.GetAllAsync();
    }
}