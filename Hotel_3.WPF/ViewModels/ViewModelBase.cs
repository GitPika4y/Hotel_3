using Hotel_3.WPF.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hotel_3.WPF.ViewModels
{

	public class ViewModelBase(INavigator navigator) : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void Navigate(ViewModelCase viewModelCase)
		{
			navigator.Navigate(viewModelCase);
			Console.WriteLine($"Now is {viewModelCase} VM");
		}

		private void OnPropertyChange([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
		{
			field = value;
			OnPropertyChange(propertyName);
		}
	}
}
