using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.UseCases.Users.Role;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_3.WPF.Views.Modal;

public partial class AddUpdateUserModal : UserControl, INotifyPropertyChanged
{
    private int _id;
    
    public string Title { get; }
    public string ConfirmButtonText { get; }

    private string _login;
    public string Login
    {
        get => _login;
        set => SetField(ref _login, value);
    }
    
    private string _password;
    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    public ObservableCollection<Role> Roles { get; } = [];
    public Role SelectedRole { get; set; }
    
    public ICommand SaveUserCommand { get; }
    
    public AddUpdateUserModal(
        IServiceProvider serviceProvider,
        string title,
        string confirmButtonText,
        User? user = null )
    {
        InitializeComponent();
        DataContext = this;
        
        Title = title;
        ConfirmButtonText = confirmButtonText;
        SaveUserCommand = new AsyncRelayCommand(SaveUser, CanSave);
        
        _ = InitializeAsync(serviceProvider, user);
    }

    private bool CanSave()
    {
        return !string.IsNullOrEmpty(Login) && 
               !string.IsNullOrEmpty(Password);
    }
    

    private async Task SaveUser()
    {
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


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}