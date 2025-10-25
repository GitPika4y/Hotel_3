using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Main.Status;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Rooms;

public class StatusViewModel : ViewModelBase
{
    private readonly IStatusUseCase _useCase;
    
    private ObservableCollection<RoomStatus> _statuses = [];
    public ObservableCollection<RoomStatus> Statuses
    {
        get => _statuses;
        set => SetProperty(ref _statuses, value);
    }

    public RoomStatus? SelectedItem { get; set; }
    public ICommand AddStatusCommand { get; }
    public ICommand UpdateStatusCommand { get; }
    
    
    public StatusViewModel(INavigator navigator, IStatusUseCase useCase) : base(navigator)
    {
        _useCase = useCase;
        AddStatusCommand = new AsyncRelayCommand(AddStatusAsync, () => true);
        UpdateStatusCommand = new AsyncRelayCommand(UpdateStatusAsync, () => SelectedItem != null);
    }

    
    public async Task LoadStatusesAsync()
    {
        var result = await _useCase.GetAllAsync();
        if (result is {IsSuccess:true, Data:not null})
        {
            Statuses.Clear();
            foreach (var status in result.Data)
            {
                Statuses.Add(status);
            }
        }
    }

    private async Task AddStatusAsync()
    {
        var result = await DialogHost.Show(new AddUpdateCategoryStatusModal(
            "Добавить статус",
            "Сохранить",
            "Название статуса"));
        var statusName = result?.ToString();
        if (statusName != null)
        {
            var newStatus = new RoomStatus
            {
                Name = statusName
            };

            var resource = await _useCase.AddAsync(newStatus);
            if(resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message, "Ок"));
            else
                await LoadStatusesAsync();
        }
    }

    private async Task UpdateStatusAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await DialogHost.Show(new AddUpdateCategoryStatusModal(
            "Обновить статус",
            "Изменить",
            "Название статуса",
            item.Name));
        var statusName = result?.ToString();
        if (statusName != null)
        {
            var updatedStatus = new RoomStatus
            {
                Id = item.Id,
                Name = statusName
            };

            var resource = await _useCase.UpdateAsync(updatedStatus);
            if(resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message, "Ок"));
            else
                await LoadStatusesAsync();
        }
    }
}