using Hotel_3.Domain.Mappers;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Models.Data;
using Hotel_3.Domain.Services.Data;
using Hotel_3.EntityFramework.Services.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hotel_3.EntityFramework.Services.Data;

public class DataService : IDataService
{
    private readonly GenericGetAllAsyncService<Room> _roomService = new();
    private readonly GenericGetAllAsyncService<RoomCategory> _roomCategoryService = new();
    private readonly GenericGetAllAsyncService<RoomStatus> _roomStatusService = new();
    
    public async Task ExportToJsonAsync(string path)
    {
        var rooms = (await _roomService.GetAllAsync(
                r => r.RoomCategory,
                r => r.RoomStatus))
            .Select(r => r.ToExport());
        
        ExportData allData = new()
        {
            Rooms = rooms,
            RoomCategories = (await _roomCategoryService.GetAllAsync()).Select(c => c.ToExport()),
            RoomStatuses = (await _roomStatusService.GetAllAsync()).Select(s => s.ToExport()),
        };

        var json = JsonConvert.SerializeObject(allData);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task ImportFromJsonAsync(string path)
    {
        var json = await File.ReadAllTextAsync(path);
        var data = JsonConvert.DeserializeObject<ExportData>(json);
        
        if(data == null) throw new InvalidDataException("Неверный формат файла");
        
        await using var context = new HotelDbContextFactory().CreateDbContext();

        await ImportDataAsync(context, data);
    }

    private async Task ImportDataAsync(HotelDbContext context, ExportData data)
    {
        foreach (var status in data.RoomStatuses)
        {
            var existingStatus = await context.RoomStatuses.FirstOrDefaultAsync(s => s.Name == status.Name);

            if (existingStatus != null) continue;
            
            var newStatus = new RoomStatus{ Name = status.Name };
            await context.RoomStatuses.AddAsync(newStatus);
        }
        await context.SaveChangesAsync();

        foreach (var category in data.RoomCategories)
        {
            var existingCategory = await context.RoomCategories.FirstOrDefaultAsync(s => s.Name == category.Name);
            
            if (existingCategory != null) continue;
            var newCategory = new RoomCategory { Name = category.Name };
            await context.RoomCategories.AddAsync(newCategory);
        }
        await context.SaveChangesAsync();

        foreach (var room in data.Rooms)
        {
            var existingRoom = await context.Rooms
                .FirstOrDefaultAsync(r => r.Floor == room.Floor && r.Number == room.Number);

            if (existingRoom != null) continue;

            var category = await context.RoomCategories
                .FirstOrDefaultAsync(c => c.Name == room.RoomCategoryName);
            var status = await context.RoomStatuses
                .FirstOrDefaultAsync(s => s.Name == room.RoomStatusName);

            if (category == null || status == null) continue;
            
            var newRoom = new Room
            {
                Floor = room.Floor,
                Number = room.Number,
                RoomCategoryId = category.Id,
                RoomStatusId = status.Id,
            };
            await context.Rooms.AddAsync(newRoom);
        }
        await context.SaveChangesAsync();
    }
}