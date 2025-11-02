using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Clients;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels;

public class ClientViewModel : ViewModelBase
{
    private readonly IClientUseCase _useCase;

    private ObservableCollection<Client> Clients { get; } = [];
    
    private readonly ICollectionView _clientsView;
    public ICollectionView ClientsView => _clientsView;
    
    public Client? SelectedItem { get; set; }

    private string _lastNameFilterText;
    public string LastNameFilterText
    {
        get => _lastNameFilterText;
        set
        {
            SetProperty(ref _lastNameFilterText, value);
            _clientsView.Refresh();
        }
    }
    
    private bool _isFiltersChecked;
    public bool IsFiltersChecked
    {
        get => _isFiltersChecked;
        set  
        {
            SetProperty(ref _isFiltersChecked, value);
            LastNameFilterText = "";
            OnPropertyChange(nameof(FiltersVisibility));
        }
    }
    
    public Visibility FiltersVisibility => _isFiltersChecked ?  Visibility.Visible : Visibility.Collapsed;


    public ICommand AddClientCommand { get; }
    public ICommand UpdateClientCommand { get; }

    public ClientViewModel(INavigator navigator, IClientUseCase useCase) : base(navigator)
    {
        _useCase = useCase;

        AddClientCommand = new AsyncRelayCommand(AddClientAsync, () => true);
        UpdateClientCommand = new AsyncRelayCommand(UpdateClientAsync, () => SelectedItem != null);
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

    private async Task AddClientAsync()
    {
        var result = await DialogHost.Show(new AddUpdateClientModal(
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

    private async Task UpdateClientAsync()
    {
        var item = SelectedItem;
        if (item is null) return;
        
        var result = await DialogHost.Show(new AddUpdateClientModal(
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