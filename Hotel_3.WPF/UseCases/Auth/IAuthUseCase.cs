using Hotel_3.Domain.Models;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Auth;

public interface IAuthUseCase
{ 
    Task<Resource<User>> SignInAsync(string login, string password);
}