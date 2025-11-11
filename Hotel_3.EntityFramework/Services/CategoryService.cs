using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class CategoryService : ICategoryService
{
    private readonly GenericAddAsyncService<RoomCategory> _addAsync = new();
    private readonly GenericGetAllAsyncService<RoomCategory> _getAll = new();
    private readonly GenericUpdateAsyncService<RoomCategory> _updateAsync = new();
    
    public async Task<RoomCategory?> AddAsync(RoomCategory entity)
    {
        return await _addAsync.AddAsync(entity);
    }
    
    public async Task<IEnumerable<RoomCategory>> GetAllAsync()
    {
        return await _getAll.GetAllAsync();
    }

    public async Task<RoomCategory?> UpdateAsync(RoomCategory entity)
    {
        return await _updateAsync.UpdateAsync(entity);
    }
}