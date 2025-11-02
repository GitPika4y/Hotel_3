using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Users.User;

public interface IUserUseCase
{
    Task<Resource<Domain.Models.User?>> AddAsync(Domain.Models.User entity);
    Task<Resource<Domain.Models.User?>> UpdateAsync(Domain.Models.User entity);
    Task<Resource<IEnumerable<Domain.Models.User>>> GetAllAsync();
}