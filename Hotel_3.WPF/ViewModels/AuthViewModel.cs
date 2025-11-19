using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Auth;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels;

internal partial class AuthViewModel(INavigator navigator, IAuthUseCase authUseCase) : ModalNavigationBase(navigator)
{
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(SignInCommand))]
	private string _login = "";

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(SignInCommand))]
	private string _password = null!;
	
	
	[RelayCommand(CanExecute = nameof(CanSignIn))]
	private async Task SignInAsync()
	{
		var result = await authUseCase.SignInAsync(Login, Password);
		if (result is { IsSuccess: false, Message: not null })
			await DialogHost.Show(new MessageModal($"{result.Message}\n{result.GetExceptionDetails()}"));
		else
			Navigate(ViewModelCase.Main);
	}

	private bool CanSignIn()
	{
		return !string.IsNullOrWhiteSpace(Login) &&
		       !string.IsNullOrWhiteSpace(Password);
	}
}