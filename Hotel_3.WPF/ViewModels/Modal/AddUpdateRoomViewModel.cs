using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.ViewModels.Modal;

public partial class AddUpdateRoomViewModel: ViewModelBase
{
    public ObservableCollection<RoomCategory> Categories { get; } = [];
    public ObservableCollection<RoomStatus> Statuses { get; } = [];
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private RoomCategory? _selectedCategory;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private RoomStatus? _selectedStatus;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int _selectedNumber;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int _selectedFloor;

    private int _id;


    partial void OnSelectedFloorChanged(int value)
    {
        SelectedNumber = value * 100 + 1;
    } 

    public AddUpdateRoomViewModel(IServiceProvider serviceProvider, Room? room = null)
    {
        _ = InitializeAsync(serviceProvider, room);
    }

    private async Task InitializeAsync(IServiceProvider serviceProvider, Room? room)
    {
         await InitializeComboBoxItemsSource(serviceProvider);
         await AssignProperties(room);
    }

    private async Task AssignProperties(Room? room)
    {
        if (room is null)
        {
            _id = 0;
            SelectedCategory = Categories.First();
            SelectedStatus = Statuses.First();
            SelectedFloor = 1;
            SelectedNumber = 101;
        }
        else
        {
            _id = room.Id;
            SelectedFloor = room.Floor;
            SelectedNumber = room.Number;
            SelectedCategory = Categories.First(c => c.Id == room.RoomCategoryId);
            SelectedStatus = Statuses.First(s => s.Id == room.RoomStatusId);
        }
    }

    private async Task InitializeComboBoxItemsSource(IServiceProvider serviceProvider)
    {
        var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        var statusService = serviceProvider.GetRequiredService<IStatusService>();

        var categories = await categoryService.GetAllAsync();
        var statuses = await statusService.GetAllAsync();

        Categories.Clear();
        Statuses.Clear();

        foreach (var category in categories)
            Categories.Add(category);

        foreach (var status in statuses)
            Statuses.Add(status);
    }

    private bool CanSave()
    {
        return SelectedCategory != null &&
               SelectedStatus != null;
    } 
    
    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        if (SelectedCategory == null || SelectedStatus == null) return;
        try
        {
            var room = new  Room
            {
                Id = _id,
                RoomCategoryId = SelectedCategory.Id,
                RoomStatusId = SelectedStatus.Id,
                Floor = SelectedFloor,
                Number = SelectedNumber,
            };
            DialogHost.CloseDialogCommand.Execute(room, null);
        }
        catch (Exception e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            await DialogHost.Show(new MessageModal($"Вы не выбрали Категорию или Статус комнаты\n{e}"));
        }
    }
}