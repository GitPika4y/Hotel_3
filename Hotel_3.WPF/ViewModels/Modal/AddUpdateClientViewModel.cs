using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels.Modal;

public partial class AddUpdateClientViewModel: ViewModelBase
{
     private int _id;
    
    public string Title { get; }
    public string ConfirmButtonText { get; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveClientCommand))]
    private string _firstName;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveClientCommand))]
    private string _lastName;
    
    [ObservableProperty]
    private string _middleName;
    
    
    public AddUpdateClientViewModel(string title, string confirmButtonText, Client? client = null)
    {
        Title = title;
        ConfirmButtonText = confirmButtonText;
        
        AssignProperties(client);
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveClient()
    {
        try
        {
            var client = new Client
            {
                Id = _id,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
            };
            DialogHost.CloseDialogCommand.Execute(client, null);
        }
        catch (Exception e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            await DialogHost.Show(new MessageModal("Ошибка при создании клиента"));
        }
    }

    private bool CanSave => !string.IsNullOrEmpty(FirstName) &&
                            !string.IsNullOrEmpty(LastName);

    private void AssignProperties(Client? client)
    {
        if (client != null)
        {
            _id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            MiddleName = client.MiddleName;
        }
        else
        {
            _id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleName = string.Empty;
        }
    }
}