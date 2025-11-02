using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Users.Role;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Users;

public class RoleViewModel : ViewModelBase
{
    private readonly IRoleUseCase _useCase;

    public ObservableCollection<Role> Roles { get; } = [];
    
    public Role? SelectedItem { get; set; }
    public ICommand AddRoleCommand { get; }
    public ICommand UpdateRoleCommand { get; }
    
    public RoleViewModel(INavigator navigator, IRoleUseCase useCase) : base(navigator)
    {
        _useCase = useCase;

        AddRoleCommand = new AsyncRelayCommand(AddRoleAsync, () => true);
        UpdateRoleCommand = new AsyncRelayCommand(UpdateRoleAsync, () => SelectedItem != null);
    }

    public async Task InitializeAsync()
    {
        await LoadRolesAsync();
    }
    
    private async Task LoadRolesAsync()
    {
        var resource = await _useCase.GetAllAsync();
        if (resource is { IsSuccess: true, Data: not null })
        {
            Roles.Clear();
            foreach (var role in resource.Data)
                Roles.Add(role);
        }
    }
    
    private async Task UpdateRoleAsync()
    {
        var item = SelectedItem;
        if (item == null) return;
        
        var result = await DialogHost.Show(new AddUpdateCategoryStatusRoleModal(
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
            var resource = await _useCase.UpdateAsync(updatedRole);
            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message));
            else
                await LoadRolesAsync();
        }
    }

    private async Task AddRoleAsync()
    {
        var result = await DialogHost.Show(new AddUpdateCategoryStatusRoleModal(
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
            
            var  resource = await _useCase.AddAsync(newRole);
            if (resource is { IsSuccess: false, Message: not null })
                await DialogHost.Show(new MessageModal(resource.Message));
            else
                await LoadRolesAsync();
        }
    }
}