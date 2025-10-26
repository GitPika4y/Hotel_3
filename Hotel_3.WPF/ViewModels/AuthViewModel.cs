using System.Windows.Input;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Auth;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels;

internal class AuthViewModel : ViewModelBase
{
	private readonly IAuthUseCase _authUseCase;
	    
	private string _login = "";
	public string Login
	{
		get => _login;
		set => SetProperty(ref _login, value);
	}

	private string _password = "";
	public string Password
	{
		get => _password;
		set => SetProperty(ref _password, value);
	}
        
	private string _errorMessage = "";
	public string ErrorMessage
	{
		get => _errorMessage;
		set => SetProperty(ref _errorMessage, value);
	}

	public ICommand SignInCommand { get; }
        
	public AuthViewModel(INavigator navigator, IAuthUseCase authUseCase) : base(navigator)
	{
		_authUseCase = authUseCase;
			
		SignInCommand = new AsyncRelayCommand(SignInAsync, CanSignIn);
	}

	private async Task SignInAsync()
	{
		var result = await _authUseCase.SignInAsync(Login, Password);
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