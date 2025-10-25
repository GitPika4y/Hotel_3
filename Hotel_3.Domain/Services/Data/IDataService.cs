namespace Hotel_3.Domain.Services.Data;

public interface IDataService
{
    Task ExportToJsonAsync(string path);
    Task ImportFromJsonAsync(string path);
}