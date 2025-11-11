using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.UseCases.Clients;
using Hotel_3.WPF.UseCases.Rooms.Room;
using Hotel_3.WPF.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.Views.Modal;

public partial class AddUpdateBookingModal : UserControl
{
    private int _id;
    
    public string Title { get; }
    public string ConfirmButtonText { get; }

    public ObservableCollection<Room> Rooms { get; } = [];
    public ObservableCollection<Client> Clients { get; } = [];
    
    public Room? SelectedRoom { get; set; }
    public Client? SelectedClient { get; set; }
    public DateTime EnterDate { get; set; } = DateTime.Now;
    public DateTime ExitDate { get; set; } = DateTime.Now;
    
    public ICommand AddUpdateCommand { get; }
    
    public AddUpdateBookingModal(IServiceProvider serviceProvider, string title, string confirmButtonText, Booking? booking = null)
    {
        InitializeComponent();

        DataContext = this;

        Title = title;
        ConfirmButtonText = confirmButtonText;
        
        AddUpdateCommand = new AsyncRelayCommand(AddUpdateAsync, CanExecute);
        
        _ = InitializeAsync(serviceProvider, booking);
    }

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

    private bool CanExecute()
    {
        return SelectedClient != null &&
               SelectedRoom != null &&
               EnterDate <= ExitDate;
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