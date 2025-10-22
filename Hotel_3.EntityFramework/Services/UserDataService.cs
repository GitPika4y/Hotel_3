using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace Hotel_3.EntityFramework.Services;

public class UserDataService : IUserDataService
{
    public async Task<User?> GetUserByLoginAsync(string login)
    {
        await using var context = new HotelDbContextFactory().CreateDbContext();
        return await context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }
}