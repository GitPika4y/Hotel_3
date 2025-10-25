﻿using System.Windows.Input;
using Hotel_3.WPF.Commands;
using Hotel_3.WPF.Navigation;
using Hotel_3.WPF.UseCases.Data;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;

namespace Hotel_3.WPF.ViewModels;

public class DataViewModel : ViewModelBase
{
    private readonly IDataUseCase _useCase;

    public ICommand ExportCommand { get; }
    public ICommand ImportCommand { get; }
    
    public DataViewModel(INavigator navigator, IDataUseCase useCase) : base(navigator)
    {
        _useCase = useCase;

        ExportCommand = new AsyncRelayCommand(Export, () => true);
        ImportCommand = new AsyncRelayCommand(Import, () => true);
    }

    private async Task Export()
    {
        var result = await _useCase.ExportDataAsync();

        if (result is {IsSuccess:false, Message: not null})
            await DialogHost.Show(new MessageModal($"{result.Message}\n{result.GetExceptionDetails()}", "Ок"));
        else
            await DialogHost.Show(new MessageModal("Данные успешно экспортированы", "Ок"));
    }

    private async Task Import()
    {
        var result = await _useCase.ImportDataAsync();

        if (result is {IsSuccess:false, Message: not null})
            await DialogHost.Show(new MessageModal($"{result.Message}\n{result.GetExceptionDetails()}", "Ок"));
        else
            await DialogHost.Show(new MessageModal("Данные успешно импортированы", "Ок"));
    }
}