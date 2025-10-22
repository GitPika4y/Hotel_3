using System.Collections.ObjectModel;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Room;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Main.Room;

namespace Hotel_3.WPF.ViewModels.Admin;

public class RoomsViewModel : ViewModelBase
{
    private readonly IRoomUseCase _useCase;

    private ObservableCollection<Room> _rooms;
    public ObservableCollection<Room> Rooms
    {
        get => _rooms;
        set => SetProperty(ref _rooms, value);
    }

    public RoomsViewModel(INavigator navigator, IRoomUseCase useCase) : base(navigator)
    {
        _useCase = useCase;
        _ = InitializeAsync();
    }
    

    public async Task InitializeAsync()
    {
        await LoadRooms();
    }
    
    private async Task LoadRooms()
    {
        var result = await _useCase.GetRoomsAsync();
        if (result is { IsSuccess: true, Data: not null })
        {
            Rooms = new ObservableCollection<Room>(result.Data);
        }
    }
}