using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Rooms.Status;
using Hotel_3.WPF.ViewModels.Modal;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Rooms;

public partial class StatusViewModel(INavigator navigator, IStatusUseCase useCase) : ModalNavigationBase(navigator)
{
    public ObservableCollection<RoomStatus> Statuses { get; } = [];

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(UpdateStatusCommand))]
    private RoomStatus? _selectedItem;
    

    public async Task LoadStatusesAsync()
    {
        var result = await useCase.GetAllAsync();
        if (result is {IsSuccess:true, Data:not null})
        {
            Statuses.Clear();
            foreach (var status in result.Data)
            {
                Statuses.Add(status);
            }
        }
    }
    
    [RelayCommand]
    private async Task AddStatusAsync()
    {
        var result = await ShowModal(new AddUpdateCategoryStatusRoleViewModel(
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

            var resource = await useCase.AddAsync(newStatus);
            if(resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message, "Ок"));
            else
                await LoadStatusesAsync();
        }
    }

    private bool CanUpdateStatus() => SelectedItem != null;
    
    [RelayCommand(CanExecute = nameof(CanUpdateStatus))]
    private async Task UpdateStatusAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await ShowModal(new AddUpdateCategoryStatusRoleViewModel(
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

            var resource = await useCase.UpdateAsync(updatedStatus);
            if(resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message, "Ок"));
            else
                await LoadStatusesAsync();
        }
    }
}