using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels.Rooms;

namespace Hotel_3.WPF.Views.Rooms;

public partial class CategoryView : UserControl
{
    public CategoryView()
    {
        InitializeComponent();
    }

    private async void CategoryView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is CategoryViewModel vm)
        {
            await vm.LoadCategoriesAsync();
        }
    }
}