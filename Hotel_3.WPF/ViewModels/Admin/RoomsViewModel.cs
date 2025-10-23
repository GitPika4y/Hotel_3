using System.Collections.ObjectModel;
using System.Windows.Input;
using Azure.Core;
using Hotel_3.Domain.DTOs;
using Hotel_3.Domain.Mappers;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Main.Room;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Admin;

public class RoomsViewModel : ViewModelBase
{
    private readonly IRoomUseCase _useCase;
    private readonly IServiceProvider _serviceProvider;

    private ObservableCollection<RoomDto> _rooms = [];
    public ObservableCollection<RoomDto> Rooms
    {
        get => _rooms;
        set => SetProperty(ref _rooms, value);
    }
    
    public RoomDto? SelectedItem { get; set; }
    public ICommand AddRoomCommand { get; } 

    public RoomsViewModel(INavigator navigator, IRoomUseCase useCase, IServiceProvider serviceProvider) : base(navigator)
    {
        _useCase = useCase;
        _serviceProvider = serviceProvider;
        AddRoomCommand = new AsyncRelayCommand(AddRoomAsync, () => true);
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
            Rooms.Clear();
            foreach (var room in result.Data)
            {
                Rooms.Add(room.ToDto());
            }
        }
    }

    private async Task AddRoomAsync()
    {
        var result = await DialogHost.Show(new AddUpdateRoomModal(_serviceProvider));
        if (result is RoomDto room)
        {
            var resource = await _useCase.AddRoomAsync(room.ToNewModel());
            if (resource is { IsSuccess: false, Message: not null, Exception: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}", "Ок"));
            else
                await LoadRooms();
        }
    }
}