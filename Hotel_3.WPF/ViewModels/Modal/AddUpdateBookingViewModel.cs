using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.UseCases.Clients;
using Hotel_3.WPF.UseCases.Rooms.Room;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.ViewModels.Modal;

public partial class AddUpdateBookingViewModel(string title, string confirmButtonText) : ViewModelBase
{
    private int _id;
    
    public string Title { get; } = title;
    public string ConfirmButtonText { get; } = confirmButtonText;

    public ObservableCollection<Room> Rooms { get; } = [];
    public ObservableCollection<Client> Clients { get; } = [];

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddUpdateCommand))]
    private Room? _selectedRoom;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddUpdateCommand))]
    private Client? _selectedClient;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddUpdateCommand))]
    private DateTime _enterDate = DateTime.Now;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddUpdateCommand))]
    private DateTime _exitDate = DateTime.Now;


    public static async Task<AddUpdateBookingViewModel> CreateAsync(IServiceProvider serviceProvider, string title,
        string confirmButtonText, Booking? booking = null)
    {
        var vm = new AddUpdateBookingViewModel(title, confirmButtonText);
        await vm.InitializeAsync(serviceProvider, booking);
        return vm;
    }

    private bool CanAddUpdate()
    {
        return SelectedClient != null &&
            SelectedRoom != null &&
            EnterDate <= ExitDate;
    }

    [RelayCommand(CanExecute = nameof(CanAddUpdate))]
    private async Task AddUpdateAsync()
    {
        if (SelectedRoom == null || SelectedClient == null) return;
        try
        {
            var booking = new Booking
            {
                Id = _id,
                EnterDate = EnterDate,
                ExitDate = ExitDate,
                RoomId = SelectedRoom.Id,
                ClientId = SelectedClient.Id,
            };
            DialogHost.CloseDialogCommand.Execute(booking, null);
        }
        catch (Exception e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            await DialogHost.Show(new MessageModal($"Ошибка при Добавлении/Обновлении Бронирования\n{e}"));
        }
    }

    private async Task InitializeAsync(IServiceProvider serviceProvider, Booking? booking)
    {
        var roomUseCase = serviceProvider.GetRequiredService<IRoomUseCase>();
        var roomResource = await roomUseCase.GetRoomsAsync();
        switch (roomResource)
        {
            case {IsSuccess: false, Message: not null}:
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new MessageModal($"{roomResource.Message}\n{roomResource.GetExceptionDetails()}"));
                break;
            case {IsSuccess: true, Data: not null}:
                var roomsFiltered = roomResource.Data
                    .Where(r => r.RoomStatus.Name is not ("Грязный" or "Занят"));
                foreach(var room in roomsFiltered)
                    Rooms.Add(room);
                break;
        }
        
        var clientService = serviceProvider.GetRequiredService<IClientUseCase>();
        var clientsResource = await clientService.GetAllAsync();
        switch (clientsResource)
        {
            case {IsSuccess: false,  Message: not null}:
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new MessageModal($"{clientsResource.Message}\n{clientsResource.GetExceptionDetails()}"));
                break;
            case {IsSuccess: true, Data: not null}:
                foreach(var client in clientsResource.Data)
                    Clients.Add(client);
                break;
        }

        if (booking == null)
        {
            _id = 0;
            EnterDate = DateTime.Now;
            ExitDate = DateTime.Now;
            SelectedRoom = null;
            SelectedClient = null;
        }
        else
        {
            _id = booking.Id;
            EnterDate = booking.EnterDate;
            ExitDate = booking.ExitDate;
            SelectedRoom = Rooms.FirstOrDefault(r=> r.Id == booking.RoomId);
            SelectedClient = Clients.FirstOrDefault(c => c.Id == booking.ClientId);
        }
    }
}