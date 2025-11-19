using System.ComponentModel;
using Hotel_3.WPF.ViewModels;

namespace Hotel_3.WPF.Navigation;

public interface INavigator : INotifyPropertyChanged
{
    ModalNavigationBase CurrentViewModel { get; set; }
    void Navigate(ViewModelCase viewModelCase);
}