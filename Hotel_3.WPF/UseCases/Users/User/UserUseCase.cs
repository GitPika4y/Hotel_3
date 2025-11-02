using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Users.User;

public class UserUseCase(IUserService service) : IUserUseCase
{
    public async Task<Resource<Domain.Models.User?>> AddAsync(Domain.Models.User entity)
    {
        try
        {
            var user = await service.AddAsync(entity);
            return Resource<Domain.Models.User?>.Success(entity);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.User?>.Fail("Ошибка при добавлении User", e);
        }
    }

    public async Task<Resource<Domain.Models.User?>> UpdateAsync(Domain.Models.User entity)
    {
        try
        {
            var user = await service.UpdateAsync(entity);
            return Resource<Domain.Models.User?>.Success(user);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.User?>.Fail("Ошибка при обновлнении User", e);
        }
    }

    public async Task<Resource<IEnumerable<Domain.Models.User>>> GetAllAsync()
    {
        try
        {
            var users =  await service.GetAllAsync(
                u => u.Role
            );
            return Resource<IEnumerable<Domain.Models.User>>.Success(users);
        }
        catch (Exception e)
        {
            return Resource<IEnumerable<Domain.Models.User>>.Fail("Ошибка при получении Users", e);
        }
    }
}