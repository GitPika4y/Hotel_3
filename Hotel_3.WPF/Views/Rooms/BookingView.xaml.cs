using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels.Rooms;

namespace Hotel_3.WPF.Views.Rooms;

public partial class BookingView : UserControl
{
    public BookingView()
    {
        InitializeComponent();
    }

    private async void BookingView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is BookingViewModel vm)
            await vm.InitializeAsync();
    }
}