using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_3.Domain.DTOs;
using Hotel_3.Domain.Mappers;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Rooms.Room;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Rooms;

public class RoomsViewModel : ViewModelBase
{
    private readonly IRoomUseCase _useCase;
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<Room> Rooms { get; } = [];
    
    public Room? SelectedItem { get; set; }
    public ICommand AddRoomCommand { get; } 
    public ICommand UpdateRoomCommand { get; }

    public RoomsViewModel(INavigator navigator, IRoomUseCase useCase, IServiceProvider serviceProvider) : base(navigator)
    {
        _useCase = useCase;
        _serviceProvider = serviceProvider;
        AddRoomCommand = new AsyncRelayCommand(AddRoomAsync, () => true);
        UpdateRoomCommand = new AsyncRelayCommand(UpdateRoomAsync, () => SelectedItem != null);
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
            Rooms.Clear();
            foreach (var room in result.Data)
            {
                Rooms.Add(room);
            }
        }
    }

    private async Task AddRoomAsync()
    {
        var result = await DialogHost.Show(new AddUpdateRoomModal(_serviceProvider));
        if (result is Room room)
        {
            var resource = await _useCase.AddRoomAsync(room.ToNewModel());
            if (resource is { IsSuccess: false, Message: not null, Exception: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}", "Ок"));
            else
                await LoadRooms();
        }
    }

    private async Task UpdateRoomAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await DialogHost.Show(new AddUpdateRoomModal(_serviceProvider, item));
        if (result is Room room)
        {
            var resource = await _useCase.UpdateRoomAsync(room);
            if (resource is { IsSuccess: false, Message: not null, Exception: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}", "Ок"));
            else
                await LoadRooms();
        }
    }
}