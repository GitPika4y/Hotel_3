using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels.Admin;

namespace Hotel_3.WPF.Views.Admin;

public partial class RoomsView : UserControl
{
    public RoomsView()
    {
        InitializeComponent();
    }

    private async void RoomsView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is RoomsViewModel vm)
        { 
            await vm.InitializeAsync();
        }
    }
}