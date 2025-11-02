using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels.Users;

namespace Hotel_3.WPF.Views.Users;

public partial class RoleView : UserControl
{
    public RoleView()
    {
        InitializeComponent();
        
    }

    private async void RoleView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is RoleViewModel vm)
            await vm.InitializeAsync();
    }
}