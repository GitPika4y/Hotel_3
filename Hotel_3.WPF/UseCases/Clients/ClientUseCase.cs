using System.Net;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Clients;

public class ClientUseCase(IClientService service) : IClientUseCase
{
    public async Task<Resource<Client?>> AddAsync(Client entity)
    {
        try
        {
            var client = await service.AddAsync(entity);
            return Resource<Client?>.Success(client);
            
        }
        catch (Exception e)
        {
            return Resource<Client?>.Fail("Ошибка при добавлении Client", e);
        }
    }

    public async Task<Resource<Client?>> UpdateAsync(Client entity)
    {
        try
        {
            var client = await service.UpdateAsync(entity);
            return Resource<Client?>.Success(client);
            
        }
        catch (Exception e)
        {
            return Resource<Client?>.Fail("Ошибка при обновлении Client", e);
        }
    }

    public async Task<Resource<IEnumerable<Client>>> GetAllAsync()
    {
        try
        {
            var clients =  await service.GetAllAsync();
            return Resource<IEnumerable<Client>>.Success(clients);
        }
        catch (Exception e)
        {
            return Resource<IEnumerable<Client>>.Fail("Ошибка при получении Clients", e);
        }
    }
}