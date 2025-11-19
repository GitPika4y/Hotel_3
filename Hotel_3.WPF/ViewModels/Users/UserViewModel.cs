using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Users.User;
using Hotel_3.WPF.ViewModels.Modal;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Users;

public partial class UserViewModel(INavigator navigator, IUserUseCase useCase, IServiceProvider serviceProvider)
    : ModalNavigationBase(navigator)
{
    public ObservableCollection<User> Users { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateUserCommand))]
    private User? _selectedItem;

    public async Task InitializeAsync()
    {
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        var result = await useCase.GetAllAsync();
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

    [RelayCommand]
    private async Task AddUserAsync()
    {
        var result = await ShowModal(new AddUpdateUserViewModel(
            serviceProvider,
            "Добавить пользователя",
            "Добавить"
            ));

        if (result is User user)
        {
            var resource = await useCase.AddAsync(user);

            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
            else
                await LoadUsersAsync();
        }
    }

    private bool CanUpdateUser() => SelectedItem != null;
    
    [RelayCommand(CanExecute = nameof(CanUpdateUser))]
    private async Task UpdateUserAsync()
    { 
        var item = SelectedItem; 
        if (item == null) return;
        
        var result = await ShowModal(new AddUpdateUserViewModel(
            serviceProvider,
            "Изменить пользователя",
            "Изменить",
            item
        ));
        
        if (result is User user)
        {
            var resource = await useCase.UpdateAsync(user);

            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
            else
                await LoadUsersAsync();
        }
    }
}