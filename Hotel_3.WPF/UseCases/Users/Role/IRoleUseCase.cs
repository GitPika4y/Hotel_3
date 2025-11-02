using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Users.Role;

public interface IRoleUseCase
{
    Task<Resource<Domain.Models.Role?>> AddAsync(Domain.Models.Role entity);
    Task<Resource<Domain.Models.Role?>> UpdateAsync(Domain.Models.Role entity);
    Task<Resource<IEnumerable<Domain.Models.Role>>> GetAllAsync();
}