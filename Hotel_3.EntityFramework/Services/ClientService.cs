using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class ClientService : IClientService
{
    private readonly GenericAddAsyncService<Client> _addService = new();
    private readonly GenericUpdateAsyncService<Client> _updateService = new();
    private readonly GenericGetAllAsyncService<Client> _getService = new();
    
    public async Task<Client?> AddAsync(Client entity)
    {
        return await _addService.AddAsync(entity);
    }

    public async Task<Client?> UpdateAsync(Client entity)
    {
        return await _updateService.UpdateAsync(entity);
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _getService.GetAllAsync();
    }
}