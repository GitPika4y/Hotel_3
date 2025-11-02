using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Users.Role;

public class RoleUseCase(IRoleService service) : IRoleUseCase
{
    public async Task<Resource<Domain.Models.Role?>> AddAsync(Domain.Models.Role entity)
    {
        try
        {
            var role = await service.AddAsync(entity);
            return Resource<Domain.Models.Role?>.Success(role);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.Role?>.Fail("Ошибка при добавлении роли", e);
        }
    }

    public async Task<Resource<Domain.Models.Role?>> UpdateAsync(Domain.Models.Role entity)
    {
        try
        {
            var role = await service.UpdateAsync(entity);
            return Resource<Domain.Models.Role?>.Success(role);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.Role?>.Fail("Ошибка при обновлении роли", e);
        }
    }

    public async Task<Resource<IEnumerable<Domain.Models.Role>>> GetAllAsync()
    {
        try
        {
            var roles = await service.GetAllAsync();
            return Resource<IEnumerable<Domain.Models.Role>>.Success(roles);
        }
        catch (Exception e)
        {
            return Resource<IEnumerable<Domain.Models.Role>>.Fail("Ошибка при получении роли", e);
        }
    }
}