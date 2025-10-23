using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class StatusService : IStatusService
{
    private readonly GenericAddAsyncService<RoomStatus> _addAsync = new();
    private readonly GenericUpdateAsyncService<RoomStatus> _updateAsync = new();
    private readonly GenericGetAllAsyncService<RoomStatus> _getAllAsync = new();
    
    public async Task<RoomStatus?> AddAsync(RoomStatus entity)
    {
        return await _addAsync.AddAsync(entity);
    }

    public async Task<RoomStatus?> UpdateAsync(RoomStatus entity)
    {
        return await _updateAsync.UpdateAsync(entity);
    }

    public async Task<IEnumerable<RoomStatus>> GetAllAsync(params Expression<Func<RoomStatus, object>>[] includes)
    {
        return await _getAllAsync.GetAllAsync(includes);
    }

    public async Task<IEnumerable<RoomStatus>> GetAllAsync()
    {
        return await _getAllAsync.GetAllAsync();
    }
}