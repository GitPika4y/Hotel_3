using System.Windows.Input;
using Hotel_3.Domain.Services.Auth;

namespace Hotel_3.WPF.Commands;

public class AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
    : ICommand
{
    private bool _isExecuting;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return !_isExecuting && canExecute();
    }

    public async void Execute(object? parameter)
    {
        if(_isExecuting) return;
        
        _isExecuting = true;
        CommandManager.InvalidateRequerySuggested();

        try
        {
            await execute();
        }
        finally
        {
            _isExecuting = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }
}