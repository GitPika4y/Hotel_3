﻿using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Auth;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Auth;

public class AuthUseCase(IUserDataService userService) : IAuthUseCase
{
    public async Task<Resource<User>> SignInAsync(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            return Resource<User>.Fail("Логин и пароль обязательны");
        
        var user = await userService.GetUserByLoginAsync(login);
        if(user == null)
            return Resource<User>.Fail("Пользователь не найдет");
        
        if(user.Password != password)
            return Resource<User>.Fail("Неверный пароль");
        
        return Resource<User>.Success(user);
    }
}