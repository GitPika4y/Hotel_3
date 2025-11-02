using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.Domain.Services.Category;
using Hotel_3.WPF.Commands;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.Views.Modal;

public partial class AddUpdateRoomModal : UserControl, INotifyPropertyChanged
{
    private ObservableCollection<RoomCategory> _categories = [];
    private ObservableCollection<RoomStatus> _statuses = [];
    private RoomCategory _selectedCategory;
    private RoomStatus _selectedStatus;
    private int _selectedNumber;
    private int _selectedFloor;

    private int _id;

    public int SelectedFloor
    {
        get => _selectedFloor;
        set
        {
            SetField(ref _selectedFloor, value);
            SelectedNumber = _selectedFloor * 100 + 1;
        }
    }

    public int SelectedNumber
    {
        get => _selectedNumber;
        set => SetField(ref _selectedNumber, value);
    }

    public ObservableCollection<RoomCategory> Categories
    {
        get => _categories;
        set => SetField(ref _categories, value);
    }

    public ObservableCollection<RoomStatus> Statuses
    {
        get => _statuses;
        set => SetField(ref _statuses, value);
    }

    public RoomCategory SelectedCategory
    {
        get => _selectedCategory;
        set => SetField(ref _selectedCategory, value);
    }

    public RoomStatus SelectedStatus
    {
        get => _selectedStatus;
        set  => SetField( ref _selectedStatus, value);
    }

    
    public ICommand SaveCommand { get; }

    public AddUpdateRoomModal(IServiceProvider serviceProvider, Room? room = null)
    {
        InitializeComponent();
        DataContext = this;
        SaveCommand = new RelayCommand(_ => Save());
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

    private async Task Save()
    {
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


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}