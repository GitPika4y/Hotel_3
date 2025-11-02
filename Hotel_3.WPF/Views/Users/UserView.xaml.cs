using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels.Users;

namespace Hotel_3.WPF.Views.Users;

public partial class UserView : UserControl
{
    public UserView()
    {
        InitializeComponent();
    }

    private async void UserView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is UserViewModel vm)
            await vm.InitializeAsync();
    }
}