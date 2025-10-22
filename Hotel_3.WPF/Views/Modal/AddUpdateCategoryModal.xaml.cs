using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.Views.Modal;

public partial class AddUpdateCategoryModal : UserControl, INotifyBase
{
    public string Title { get; }
    public string ButtonText { get; }
    
    private string _categoryName;
    public string CategoryName
    {
        get => _categoryName;
        set => SetProperty(ref _categoryName, value);
    }
    
    public AddUpdateCategoryModal(string title, string buttonText, string? categoryName = null)
    {
        InitializeComponent();
        DataContext = this;
        Title = title;
        ButtonText = buttonText;
        _categoryName = categoryName ?? string.Empty;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SetProperty<T>(ref T field, T value, string propertyName = "")
    {
        field = value;
        OnPropertyChanged(propertyName);
    }
}