using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel_3.WPF.Commands;

internal class RelayCommand(Action<object?> executeAction, Func<object?, bool>? canExecute = null)
	: ICommand
{
	public event EventHandler? CanExecuteChanged
	{
		add => CommandManager.RequerySuggested += value;
		remove => CommandManager.RequerySuggested -= value;
	}


	public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;
		
	public void Execute(object? parameter) => executeAction(parameter);
}