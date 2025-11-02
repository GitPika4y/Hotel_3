using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Users.User;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Users;

public class UserViewModel : ViewModelBase
{
    private readonly IUserUseCase _useCase;
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<User> Users { get; } = [];
    
    public User? SelectedItem { get; set; }
    
    public ICommand AddUserCommand { get; }
    public ICommand UpdateUserCommand { get; }
    
    public UserViewModel(INavigator navigator, IUserUseCase useCase, IServiceProvider serviceProvider): base(navigator)
    {
        _useCase = useCase;
        _serviceProvider = serviceProvider;
        
        AddUserCommand = new AsyncRelayCommand(AddUserAsync, () => true);
        UpdateUserCommand = new AsyncRelayCommand(UpdateUserAsync, () => SelectedItem != null);
        
        _ = InitializeAsync();
    }

    public async Task InitializeAsync()
    {
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        var result = await _useCase.GetAllAsync();
        switch (result)
        {
            case {IsSuccess:false, Message: not null}:
                await DialogHost.Show(new MessageModal($"{result.Message}\n{result.GetExceptionDetails()}"));
                break;
            case { IsSuccess: true, Data: not null }:
            {
                Users.Clear();
                foreach (var user in result.Data)
                    Users.Add(user);
                break;
            }
        }
    }

    private async Task AddUserAsync()
    {
        var result = await DialogHost.Show(new AddUpdateUserModal(
            _serviceProvider,
            "Добавить пользователя",
            "Добавить"
            ));

        if (result is User user)
        {
            var resource = await _useCase.AddAsync(user);

            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
            else
                await LoadUsersAsync();
        }
    }

    private async Task UpdateUserAsync()
    { 
        var item = SelectedItem; 
        if (item == null) return;
        
        var result = await DialogHost.Show(new AddUpdateUserModal(
            _serviceProvider,
            "Изменить пользователя",
            "Изменить",
            item
        ));
        
        if (result is User user)
        {
            var resource = await _useCase.UpdateAsync(user);

            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
            else
                await LoadUsersAsync();
        }
    }
}