using Hotel_3.WPF.Navigation;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels;

public class ModalNavigationBase(INavigator navigator) : ViewModelBase
{
	protected void Navigate(ViewModelCase viewModelCase)
	{
		navigator.Navigate(viewModelCase);
		Console.WriteLine($"Now is {viewModelCase} VM");
	}
	
	protected async Task<object?> ShowModal(ViewModelBase viewModel)
	{
		return await DialogHost.Show(viewModel);
	}
}