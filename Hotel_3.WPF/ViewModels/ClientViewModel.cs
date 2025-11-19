using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Clients;
using Hotel_3.WPF.ViewModels.Modal;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;
using AsyncRelayCommand = Hotel_3.WPF.Commands.AsyncRelayCommand;

namespace Hotel_3.WPF.ViewModels;

public partial class ClientViewModel : ModalNavigationBase
{
    private readonly IClientUseCase _useCase;

    private ObservableCollection<Client> Clients { get; } = [];
    
    private readonly ICollectionView _clientsView;
    public ICollectionView ClientsView => _clientsView;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(UpdateClientCommand))]
    private Client? _selectedItem;

    [ObservableProperty]
    private string _lastNameFilterText;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FiltersVisibility))]
    private bool _isFiltersChecked;
    
    partial void OnIsFiltersCheckedChanged(bool value)
    {
        LastNameFilterText = "";
    }

    partial void OnLastNameFilterTextChanged(string value)
    {
        _clientsView.Refresh();
    }
    
    public Visibility FiltersVisibility => IsFiltersChecked ?  Visibility.Visible : Visibility.Collapsed;

    
    public ClientViewModel(INavigator navigator, IClientUseCase useCase) : base(navigator)
    {
        _useCase = useCase;
        
        _clientsView = CollectionViewSource.GetDefaultView(Clients);
        _clientsView.Filter = FilterClients;
        
        _ = InitializeAsync();
    }
    
    private bool FilterClients(object obj)
    {
        if (obj is not Client client) return false;
        
        if(string.IsNullOrEmpty(LastNameFilterText)) return true;

        return client.LastName.Contains(LastNameFilterText, StringComparison.OrdinalIgnoreCase);
    }

    public async Task InitializeAsync()
    {
        await LoadClientsAsync();
    }

    private async Task LoadClientsAsync()
    {
        var resource = await _useCase.GetAllAsync();
        switch (resource)
        {
            case {IsSuccess: false, Message:not null}:
                await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
                break;
            case {IsSuccess:true, Data: not null}:
                Clients.Clear();
                foreach (var client in resource.Data)
                {
                    Clients.Add(client);
                }
                break;
        }
    }

    [RelayCommand]
    private async Task AddClientAsync()
    {
        var result = await ShowModal(new AddUpdateClientViewModel(
            "Добавить клиента",
            "Добавить"
            ));
        if (result is Client client)
        {
            var resource = await _useCase.AddAsync(client);
            switch (resource)
            {
                case  {IsSuccess: false, Message:not null}:
                    await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
                    break;
                case {IsSuccess:true, Data: not null}:
                    await LoadClientsAsync();
                    break;
            }
        }
    }

    private bool CanUpdateClient() => SelectedItem != null;

    [RelayCommand(CanExecute = nameof(CanUpdateClient))]
    private async Task UpdateClientAsync()
    {
        var item = SelectedItem;
        if (item is null) return;
        
        var result = await ShowModal(new AddUpdateClientViewModel(
            "Добавить клиента",
            "Добавить",
            item
        ));
        if (result is Client client)
        {
            var resource = await _useCase.UpdateAsync(client);
            switch (resource)
            {
                case  {IsSuccess: false, Message:not null}:
                    await DialogHost.Show(new MessageModal($"{resource.Message}\n{resource.GetExceptionDetails()}"));
                    break;
                case {IsSuccess:true, Data: not null}:
                    await LoadClientsAsync();
                    break;
            }
        }
    }
}