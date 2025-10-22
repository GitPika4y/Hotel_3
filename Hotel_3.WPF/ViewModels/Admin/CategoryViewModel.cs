using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Category;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Main.Category;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Admin;

public class CategoryViewModel : ViewModelBase
{
    private readonly ICategoryUseCase _useCase;
    
    private ObservableCollection<RoomCategory> _categories;
    public ObservableCollection<RoomCategory> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    public RoomCategory? SelectedItem { get; set; } = null;
    public string ErrorMessage { get; private set; }
    
    public ICommand AddCategoryCommand { get; }
    public ICommand UpdateCategoryCommand { get; }

    public CategoryViewModel(INavigator navigator, ICategoryUseCase useCase) : base(navigator)
    {
        _useCase = useCase;
        Categories = [];
        AddCategoryCommand = new AsyncRelayCommand(AddCategory, () => true);
        UpdateCategoryCommand = new AsyncRelayCommand(UpdateCategory, () => SelectedItem != null);
        _ = LoadCategories();
    }

    private async Task UpdateCategory()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await DialogHost.Show(new AddUpdateCategoryModal("Обновить категорию", "Изменить", item.Name));
        var updatedCategoryName = result?.ToString();
        if (updatedCategoryName != null)
        {
            var updatedItem = new RoomCategory
            {
                Id = item.Id,
                Name = updatedCategoryName
            };
            
            var resource = await _useCase.UpdateAsync(updatedItem);
            if (resource is { IsSuccess: false, Message: not null })
                ErrorMessage = resource.Message;
            else
                await LoadCategories();
        }
    }

    private async Task LoadCategories()
    {
        var result = await _useCase.GetAllAsync();
        if (result is { IsSuccess: true, Data: not null })
        {
            Categories.Clear();
            foreach (var category in result.Data)
            {
                Categories.Add(category);
            }
        }
    }

    private async Task AddCategory()
    {
        var result = await DialogHost.Show(new AddUpdateCategoryModal("Добавить категорию", "Сохранить"));
        var categoryName = result?.ToString();
        if (categoryName != null)
        {
            var newCategory = new RoomCategory
            {
                Name = categoryName
            };
            
            var resource = await _useCase.AddAsync(newCategory);
            if (resource is { IsSuccess: true, Message: not null })
                ErrorMessage = resource.Message;
            else
                await LoadCategories();
        }

    }
}