using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels;

namespace Hotel_3.WPF.Views;

public partial class ClientView : UserControl
{
    public ClientView()
    {
        InitializeComponent();
    }


    private async void ClientView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ClientViewModel vm)
            await vm.InitializeAsync();
    }
}