using System.ComponentModel;
using System.Windows.Controls;

namespace Hotel_3.WPF.Views.Modal;

public partial class AddUpdateCategoryStatusModal : UserControl, INotifyPropertyChanged
{
    public string Title { get; }
    public string ButtonText { get; }
    public string HintText { get; }
    
    private string _categoryName;
    public string CategoryName
    {
        get => _categoryName;
        set => SetProperty(ref _categoryName, value);
    }
    
    public AddUpdateCategoryStatusModal(string title, string buttonText, string hintText, string? categoryName = null)
    {
        InitializeComponent();
        DataContext = this;
        Title = title;
        ButtonText = buttonText;
        HintText = hintText;
        _categoryName = categoryName ?? string.Empty;
    }
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetProperty<T>(ref T field, T value, string propertyName = "")
    {
        field = value;
        OnPropertyChanged(propertyName);
    }
}