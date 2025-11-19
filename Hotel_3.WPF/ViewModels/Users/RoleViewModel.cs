using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Users.Role;
using Hotel_3.WPF.ViewModels.Modal;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Users;

public partial class RoleViewModel(INavigator navigator, IRoleUseCase useCase) : ModalNavigationBase(navigator)
{
    public ObservableCollection<Role> Roles { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateRoleCommand))]
    private Role? _selectedItem;

    public async Task InitializeAsync()
    {
        await LoadRolesAsync();
    }
    
    private async Task LoadRolesAsync()
    {
        var resource = await useCase.GetAllAsync();
        if (resource is { IsSuccess: true, Data: not null })
        {
            Roles.Clear();
            foreach (var role in resource.Data)
                Roles.Add(role);
        }
    }

    private bool CanUpdateRole() => SelectedItem != null;
    
    [RelayCommand(CanExecute = nameof(CanUpdateRole))]
    private async Task UpdateRoleAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await ShowModal(new AddUpdateCategoryStatusRoleViewModel(
            "Обновить роль",
            "Изменить",
            "Название роли",
            item.Name
            ));
        
        var updatedRoleName = result?.ToString();
        if (updatedRoleName != null)
        {
            var updatedRole = new Role
            {
                Id = item.Id,
                Name = updatedRoleName,
            };
            var resource = await useCase.UpdateAsync(updatedRole);
            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message));
            else
                await LoadRolesAsync();
        }
    }

    [RelayCommand]
    private async Task AddRoleAsync()
    {
        var result = await ShowModal(new AddUpdateCategoryStatusRoleViewModel(
            "Добавить роль",
            "Добавить",
            "Название роли"
            ));
        
        var roleName = result?.ToString();
        if (roleName != null)
        {
            var newRole = new Role
            {
                Name = roleName,
            };
            
            var  resource = await useCase.AddAsync(newRole);
            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message));
            else
                await LoadRolesAsync();
        }
    }
}