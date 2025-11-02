using Hotel_3.Domain.Models;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Clients;

public interface IClientUseCase
{
    Task<Resource<Client?>> AddAsync(Client entity);
    Task<Resource<Client?>> UpdateAsync(Client entity);
    Task<Resource<IEnumerable<Client>>> GetAllAsync();
}