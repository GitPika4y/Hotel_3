using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class RoleService : IRoleService
{
    private readonly GenericAddAsyncService<Role> _addService = new();
    private readonly GenericUpdateAsyncService<Role> _updateService = new();
    private readonly GenericGetAllAsyncService<Role> _getAllService = new();
    
    public async Task<Role?> AddAsync(Role entity)
    {
        return await _addService.AddAsync(entity);
    }

    public async Task<Role?> UpdateAsync(Role entity)
    {
        return await _updateService.UpdateAsync(entity);
    }

    public async Task<IEnumerable<Role>> GetAllAsync(params Expression<Func<Role, object>>[] includes)
    {
        return await _getAllService.GetAllAsync(includes);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _getAllService.GetAllAsync([]);
    }
}