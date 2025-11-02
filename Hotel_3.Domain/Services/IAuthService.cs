using Hotel_3.Domain.Models;

namespace Hotel_3.Domain.Services;

public interface IAuthService
{
    Task<User?> GetUserByLoginAsync(string login);
}