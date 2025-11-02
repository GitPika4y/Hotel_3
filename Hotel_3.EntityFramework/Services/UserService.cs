using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hotel_3.EntityFramework.Services;

public class UserService : IUserService
{
    private readonly GenericAddAsyncService<User> _addService = new();
    private readonly GenericUpdateAsyncService<User> _updateService = new();
    private readonly GenericGetAllAsyncService<User> _getAllService = new();
    
    public async Task<User?> AddAsync(User entity)
    {
        return await _addService.AddAsync(entity);
    }

    public async Task<User?> UpdateAsync(User entity)
    {
        return await _updateService.UpdateAsync(entity);
    }

    public Task<IEnumerable<User>> GetAllAsync(params Expression<Func<User, object>>[] includes)
    {
        return _getAllService.GetAllAsync(includes);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return _getAllService.GetAllAsync();
    }
}