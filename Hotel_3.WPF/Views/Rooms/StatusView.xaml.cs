using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels.Rooms;

namespace Hotel_3.WPF.Views.Rooms;

public partial class StatusView : UserControl
{
    public StatusView()
    {
        InitializeComponent();
    }

    private async void StatusView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is StatusViewModel vm)
        {
            await vm.LoadStatusesAsync();
        }
    }
}