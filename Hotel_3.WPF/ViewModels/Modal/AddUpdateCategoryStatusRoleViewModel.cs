using CommunityToolkit.Mvvm.ComponentModel;

namespace Hotel_3.WPF.ViewModels.Modal;

public partial class AddUpdateCategoryStatusRoleViewModel(
    string title,
    string buttonText,
    string hintText,
    string? categoryName = null)
    : ViewModelBase
{
    public string Title { get; } = title;
    public string ButtonText { get; } = buttonText;
    public string HintText { get; } = hintText;

    [ObservableProperty]
    private string _categoryName = categoryName ?? string.Empty;
}