using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Auth;
using Hotel_3.Domain.Services.Category;
using Hotel_3.Domain.Services.Room;
using Hotel_3.EntityFramework.Services;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Auth;
using Hotel_3.WPF.UseCases.Main.Category;
using Hotel_3.WPF.UseCases.Main.Room;
using Hotel_3.WPF.ViewModels;
using Hotel_3.WPF.ViewModels.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.Services;

public static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        //Services first
        services.AddTransient<IUserDataService, UserDataService>();
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<ICategoryService, CategoryService>();
		
        //UseCases second
        services.AddTransient<IAuthUseCase, AuthUseCase>();
        services.AddTransient<IRoomUseCase, RoomUseCase>();
        services.AddTransient<ICategoryUseCase, CategoryUseCase>();
		
        //VMs third
        services.AddTransient<ViewModelBase>();
        services.AddTransient<AuthViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<RoomsViewModel>();
        services.AddTransient<CategoryViewModel>();

        //Singleton last
        services.AddSingleton<INavigator, Navigator>();
    }
}