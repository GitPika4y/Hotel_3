using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel_3.Domain.Models;
using Hotel_3.WPF.Commands;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.Views.Modal;

public partial class AddUpdateClientModal : UserControl, INotifyPropertyChanged
{
    private int _id;
    
    public string Title { get; }
    public string ConfirmButtonText { get; }

    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set => SetField(ref _firstName, value);
    }
    
    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => SetField(ref _lastName, value);
    }
    
    private string _middleName;
    public string MiddleName
    {
        get => _middleName;
        set => SetField(ref _middleName, value);
    }
    
    public ICommand SaveClientCommand { get; }
    
    
    public AddUpdateClientModal(string title, string confirmButtonText, Client? client = null)
    {
        InitializeComponent();
        DataContext = this;

        Title = title;
        ConfirmButtonText = confirmButtonText;

        SaveClientCommand = new AsyncRelayCommand(SaveClient, () => CanSave);
        
        AssignProperties(client);
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}