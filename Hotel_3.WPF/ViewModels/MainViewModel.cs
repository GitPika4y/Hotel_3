using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.ViewModels.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.ViewModels;

public class MainViewModel(INavigator navigator, IServiceProvider serviceProvider) : ViewModelBase(navigator)
{
    public RoomsViewModel RoomsViewModel { get; } = serviceProvider.GetRequiredService<RoomsViewModel>();
    public CategoryViewModel CategoryViewModel { get; } = serviceProvider.GetRequiredService<CategoryViewModel>();
    public StatusViewModel StatusViewModel { get; } = serviceProvider.GetRequiredService<StatusViewModel>();
    public DataViewModel DataViewModel { get; } = serviceProvider.GetRequiredService<DataViewModel>();
}