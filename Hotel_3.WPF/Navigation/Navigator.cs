using Hotel_3.WPF.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.Navigation
{
    public class Navigator(IServiceProvider serviceProvider) : INavigator
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ModalNavigationBase _currentViewModel;

        public ModalNavigationBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel == value) return;
                _currentViewModel = value;
                OnPropertyChange();
            }
        }

        public void Navigate(ViewModelCase viewModelCase)
        {
            ModalNavigationBase viewModel = viewModelCase switch
			{
				ViewModelCase.Auth => serviceProvider.GetRequiredService<AuthViewModel>(),
                ViewModelCase.Main => serviceProvider.GetRequiredService<MainViewModel>(),
                _ => throw new Exception("Unknown ViewModelCase")
			};

            CurrentViewModel = viewModel;
        }

        private void OnPropertyChange([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
