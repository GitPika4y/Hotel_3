using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.ViewModels.Modal;

public partial class AddUpdateUserViewModel: ViewModelBase
{
    private int _id;
    
    public string Title { get; }
    public string ConfirmButtonText { get; }

    public ObservableCollection<Role> Roles { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private string _login;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private string _password;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private Role? _selectedRole;


    public AddUpdateUserViewModel(
        IServiceProvider serviceProvider,
        string title,
        string confirmButtonText,
        User? user = null )
    {
        Title = title;
        ConfirmButtonText = confirmButtonText;
        
        _ = InitializeAsync(serviceProvider, user);
    }

    private bool CanSave()
    {
        return !string.IsNullOrEmpty(Login) &&
               !string.IsNullOrEmpty(Password) &&
               SelectedRole != null;
    }
    

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveUser()
    {
        if (SelectedRole == null) return;
        try
        {
            var newUser = new User
            {
                Id = _id,
                Login = Login,
                Password = Password,
                RoleId = SelectedRole.Id
            };
            DialogHost.CloseDialogCommand.Execute(newUser, null);
        }
        catch (Exception e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            await DialogHost.Show(new MessageModal("Вы не выбрали Роль пользователя"));
        }
    }

    private async Task InitializeAsync(IServiceProvider serviceProvider, User? user)
    {
        await InitializeComboBoxItemsSourceAsync(serviceProvider);
        AssignProperties(user);
    }

    private void AssignProperties(User? user)
    {
        if (user == null)
        {
            _id = 0;
            Login = "";
            Password = "";
            SelectedRole = Roles.First();
        }
        else
        {
            _id = user.Id;
            Login = user.Login;
            Password = user.Password;
            SelectedRole = Roles.First(r => r.Id == user.RoleId);
        }
    }

    private async Task InitializeComboBoxItemsSourceAsync(IServiceProvider serviceProvider)
    {
        var roleService = serviceProvider.GetRequiredService<IRoleService>();

        var resource = await roleService.GetAllAsync();
        
        Roles.Clear();
        foreach (var role in resource)
            Roles.Add(role);
    }
}