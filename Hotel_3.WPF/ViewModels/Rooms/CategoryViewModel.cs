using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Rooms.Category;
using Hotel_3.WPF.ViewModels.Modal;
using Hotel_3.WPF.Views;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Rooms;

public partial class CategoryViewModel(INavigator navigator, ICategoryUseCase useCase) : ModalNavigationBase(navigator)
{
    public ObservableCollection<RoomCategory> Categories { get; } = [];
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCategoryCommand))]
    private RoomCategory? _selectedItem;

    public async Task LoadCategoriesAsync()
    {
        var result = await useCase.GetAllAsync();
        if (result is { IsSuccess: true, Data: not null })
        {
            Categories.Clear();
            foreach (var category in result.Data)
            {
                Categories.Add(category);
            }
        }
    }

    private bool CanUpdateCategory() => SelectedItem != null;
    
    [RelayCommand(CanExecute = nameof(CanUpdateCategory))]
    private async Task UpdateCategory()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await ShowModal(new AddUpdateCategoryStatusRoleViewModel(
            "Обновить категорию",
            "Изменить",
            "Название категории",
            item.Name));
        var updatedCategoryName = result?.ToString();
        if (updatedCategoryName != null)
        {
            var updatedItem = new RoomCategory
            {
                Id = item.Id,
                Name = updatedCategoryName
            };
            
            var resource = await useCase.UpdateAsync(updatedItem);
            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message));
            else
                await LoadCategoriesAsync();
        }
    }

    [RelayCommand]
    private async Task AddCategory()
    {
        var result = await ShowModal(new AddUpdateCategoryStatusRoleViewModel(
            "Добавить категорию",
            "Сохранить",
            "Название категории"));
        var categoryName = result?.ToString();
        if (categoryName != null)
        {
            var newCategory = new RoomCategory
            {
                Name = categoryName
            };
            
            var resource = await useCase.AddAsync(newCategory);
            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message, "Ок"));
            else
                await LoadCategoriesAsync();
        }
    }
}