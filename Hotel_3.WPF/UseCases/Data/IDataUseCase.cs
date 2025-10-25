using Hotel_3.Domain.Models.Data;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Data;

public interface IDataUseCase
{
    Task<Resource<ExportData>> ExportDataAsync();
    Task<Resource<ExportData>> ImportDataAsync();
}