using Hotel_3.Domain.Models;

namespace Hotel_3.Domain.Services.Auth;

public interface IUserDataService
{
    Task<User?> GetUserByLoginAsync(string login);
}