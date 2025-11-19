using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Hotel_3.WPF.Services;

namespace Hotel_3.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private readonly ServiceProvider _serviceProvider;

	public App()
	{
		var services = new ServiceCollection();
        ServiceConfiguration.ConfigureServices(services);
		_serviceProvider = services.BuildServiceProvider();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		var navigator = _serviceProvider.GetRequiredService<INavigator>();
		
		navigator.Navigate(ViewModelCase.Auth);
		
		MainWindow mainWindow = new()
		{
			DataContext = navigator
		};
		mainWindow.Show();
	}
}