using Hotel_3.Domain.Services;
using Hotel_3.Domain.Services.Data;
using Hotel_3.EntityFramework.Services;
using Hotel_3.EntityFramework.Services.Data;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Auth;
using Hotel_3.WPF.UseCases.Clients;
using Hotel_3.WPF.UseCases.Data;
using Hotel_3.WPF.UseCases.Rooms.Booking;
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
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IBookingService, BookingService>();
		
        //UseCases second
        services.AddTransient<IAuthUseCase, AuthUseCase>();
        services.AddTransient<IRoomUseCase, RoomUseCase>();
        services.AddTransient<ICategoryUseCase, CategoryUseCase>();
        services.AddTransient<IStatusUseCase, StatusUseCase>();
        services.AddTransient<IDataUseCase, DataUseCase>();
        services.AddTransient<IRoleUseCase, RoleUseCase>();
        services.AddTransient<IUserUseCase, UserUseCase>();
        services.AddTransient<IClientUseCase, ClientUseCase>();
        services.AddTransient<IBookingUseCase, BookingUseCase>();
		
        //VMs third
        services.AddTransient<ModalNavigationBase>();
        services.AddTransient<AuthViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<DataViewModel>();
        services.AddTransient<RoomsViewModel>();
        services.AddTransient<CategoryViewModel>();
        services.AddTransient<StatusViewModel>();
        services.AddTransient<RoleViewModel>();
        services.AddTransient<UserViewModel>();
        services.AddTransient<ClientViewModel>();
        services.AddTransient<BookingViewModel>();

        //Singleton last
        services.AddSingleton<INavigator, Navigator>();
    }
}