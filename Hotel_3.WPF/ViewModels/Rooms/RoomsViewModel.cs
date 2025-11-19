using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.DTOs;
using Hotel_3.Domain.Mappers;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Rooms.Room;
using Hotel_3.WPF.ViewModels.Modal;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Rooms;

public partial class RoomsViewModel(INavigator navigator, IRoomUseCase useCase, IServiceProvider serviceProvider)
    : ModalNavigationBase(navigator)
{
    public ObservableCollection<Room> Rooms { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateRoomCommand))]
    private Room? _selectedItem;


    public async Task InitializeAsync()
    {
        await LoadRooms();
    }
    
    private async Task LoadRooms()
    {
        var result = await useCase.GetRoomsAsync();
        if (result is { IsSuccess: true, Data: not null })
        {
            Rooms.Clear();
            foreach (var room in result.Data)
            {
                Rooms.Add(room);
            }
        }
    }

    [RelayCommand]
    private async Task AddRoomAsync()
    {
        var result = await ShowModal(new AddUpdateRoomViewModel(serviceProvider));
        if (result is Room room)
        {
            var resource = await useCase.AddRoomAsync(room.ToNewModel());
            if (resource is { IsSuccess: false, Message: not null, Exception: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
            else
                await LoadRooms();
        }
    }

    private bool CanUpdateRoom => SelectedItem != null;
    
    [RelayCommand(CanExecute = nameof(CanUpdateRoom))]
    private async Task UpdateRoomAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await ShowModal(new AddUpdateRoomViewModel(serviceProvider, item));
        if (result is Room room)
        {
            var resource = await useCase.UpdateRoomAsync(room);
            if (resource is { IsSuccess: false, Message: not null, Exception: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
            else
                await LoadRooms();
        }
    }
}