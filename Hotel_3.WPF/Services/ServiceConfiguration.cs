using System.Xml.Serialization;
using Hotel_3.Domain.Services;
using Hotel_3.Domain.Services.Category;
using Hotel_3.Domain.Services.Data;
using Hotel_3.EntityFramework.Services;
using Hotel_3.EntityFramework.Services.Data;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Auth;
using Hotel_3.WPF.UseCases.Data;
using Hotel_3.WPF.UseCases.Rooms.Category;
using Hotel_3.WPF.UseCases.Rooms.Room;
using Hotel_3.WPF.UseCases.Rooms.Status;
using Hotel_3.WPF.UseCases.Users.Role;
using Hotel_3.WPF.UseCases.Users.User;
using Hotel_3.WPF.ViewModels;
using Hotel_3.WPF.ViewModels.Rooms;
using Hotel_3.WPF.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.Services;

public static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        //Services first
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IStatusService, StatusService>();
        services.AddTransient<IDataService, DataService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IUserService, UserService>();
		
        //UseCases second
        services.AddTransient<IAuthUseCase, AuthUseCase>();
        services.AddTransient<IRoomUseCase, RoomUseCase>();
        services.AddTransient<ICategoryUseCase, CategoryUseCase>();
        services.AddTransient<IStatusUseCase, StatusUseCase>();
        services.AddTransient<IDataUseCase, DataUseCase>();
        services.AddTransient<IRoleUseCase, RoleUseCase>();
        services.AddTransient<IUserUseCase, UserUseCase>();
		
        //VMs third
        services.AddTransient<ViewModelBase>();
        services.AddTransient<AuthViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<DataViewModel>();
        services.AddTransient<RoomsViewModel>();
        services.AddTransient<CategoryViewModel>();
        services.AddTransient<StatusViewModel>();
        services.AddTransient<RoleViewModel>();
        services.AddTransient<UserViewModel>();

        //Singleton last
        services.AddSingleton<INavigator, Navigator>();
    }
}