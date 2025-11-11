using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Rooms.Booking;
using Hotel_3.WPF.UseCases.Rooms.Room;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Rooms;

public class BookingViewModel: ViewModelBase
{
    private readonly IBookingUseCase _useCase;
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<Booking> Bookings { get; } = [];
    public Booking? SelectedItem { get; set; }
    
    public ICommand AddBookingCommand { get; }
    public ICommand UpdateBookingCommand { get; }
    
    public BookingViewModel(INavigator navigator, IBookingUseCase useCase, IServiceProvider serviceProvider): base(navigator)
    {
        _useCase = useCase;
        _serviceProvider = serviceProvider;
        
        AddBookingCommand = new AsyncRelayCommand(AddBookingAsync, () => true);
        UpdateBookingCommand = new AsyncRelayCommand(UpdateBookingAsync, () => SelectedItem != null);
        
        _ = InitializeAsync();
    }

    public async Task InitializeAsync()
    {
        await LoadBookingsAsync();
    }

    private async Task LoadBookingsAsync()
    {
        var resource = await _useCase.GetAllAsync();
        switch (resource)
        {
            case {IsSuccess: false, Message: not null}:
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
                break;
            case {IsSuccess: true, Data: not null}:
                Bookings.Clear();
                foreach(var booking in resource.Data)
                    Bookings.Add(booking);
                break;
        }
    }
    
    private async Task AddBookingAsync()
    {
        var result = await DialogHost.Show(new AddUpdateBookingModal(
            _serviceProvider,
            "Добавить бронирование",
            "Добавить"
            ));
        if (result is Booking booking)
        {
            var resource = await _useCase.AddAsync(booking);
            switch (resource)
            {
                case {IsSuccess: false, Message: not null}:
                    await DialogHost.Show(new MessageModal($"Ошибка при добавлении Бронирования.\n{resource.Message}"));
                    break;
                case {IsSuccess: true, Data: not null}:
                    await LoadBookingsAsync();
                    break;
            }
        }
    }

    private async Task UpdateBookingAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await DialogHost.Show(new AddUpdateBookingModal(
            _serviceProvider,
            "Изменить бронирование",
            "Изменить",
            item
        ));
        if (result is Booking booking)
        {
            var resource = await _useCase.UpdateAsync(booking);
            switch (resource)
            {
                case {IsSuccess: false, Message: not null}:
                    await DialogHost.Show(new MessageModal($"Ошибка при добавлении Бронирования.\n{resource.Message}"));
                    break;
                case {IsSuccess: true, Data: not null}:
                    await LoadBookingsAsync();
                    break;
            }
        }
    }
}