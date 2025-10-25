using Hotel_3.Domain.Models.Data;
using Hotel_3.Domain.Services.Data;
using Hotel_3.WPF.Utils;
using Hotel_3.WPF.Views.Modal;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace Hotel_3.WPF.UseCases.Data;

public class DataUseCase(IDataService service) : IDataUseCase
{
    public async Task<Resource<ExportData>> ExportDataAsync()
    {
        try
        {
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = ".json",
            };

            if (dialog.ShowDialog() == false) 
                return Resource<ExportData>.Fail("Путь сохранения экспорта не был выбран");    
            
            await service.ExportToJsonAsync(dialog.FileName);
            return Resource<ExportData>.Success($"Данные экспортированы в {dialog.FileName}");
            
        }
        catch (Exception e)
        {
            return Resource<ExportData>.Fail(e.Message, e);
        }
    }

    public async Task<Resource<ExportData>> ImportDataAsync()
    {
        try
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = ".json",
            };
            
            if (dialog.ShowDialog() == false)
                return Resource<ExportData>.Fail("Файл импорта не выбран");

            var result = await DialogHost.Show(new ConfirmModal(
                "Подтверждение импорта",
                "Объекты из файла будут импортированы, лишь с добавлением новых сущностей, который нет в базе данных")
            );
            
            if (result is false)
                return Resource<ExportData>.Fail("Операция импорта была прервана");
            
            await service.ImportFromJsonAsync(dialog.FileName);
            return Resource<ExportData>.Success("Данные успешно импортированы");
        }
        catch (Exception e)
        {
            return Resource<ExportData>.Fail(e.Message, e);
        }
    }
}