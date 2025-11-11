using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.ViewModels.Rooms;
using Hotel_3.WPF.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.ViewModels;

public class MainViewModel(INavigator navigator, IServiceProvider serviceProvider) : ViewModelBase(navigator)
{
    public RoomsViewModel RoomsViewModel { get; } = serviceProvider.GetRequiredService<RoomsViewModel>();
    public CategoryViewModel CategoryViewModel { get; } = serviceProvider.GetRequiredService<CategoryViewModel>();
    public StatusViewModel StatusViewModel { get; } = serviceProvider.GetRequiredService<StatusViewModel>();
    public DataViewModel DataViewModel { get; } = serviceProvider.GetRequiredService<DataViewModel>();
    public UserViewModel UserViewModel { get; } = serviceProvider.GetRequiredService<UserViewModel>();
    public RoleViewModel RoleViewModel { get; } = serviceProvider.GetRequiredService<RoleViewModel>();
    public ClientViewModel ClientViewModel { get; } = serviceProvider.GetRequiredService<ClientViewModel>();
    public  BookingViewModel BookingViewModel { get; } = serviceProvider.GetRequiredService<BookingViewModel>();
}