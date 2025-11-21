using Azure.Core.Pipeline;
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
    private readonly GenericGetAllIncludeAsyncService<Room> _roomService = new();
    private readonly GenericGetAllAsyncService<RoomCategory> _roomCategoryService = new();
    private readonly GenericGetAllAsyncService<RoomStatus> _roomStatusService = new();
    private readonly GenericGetAllAsyncService<Client> _clientService = new();
    private readonly GenericGetAllAsyncService<Role> _roleService = new();
    private readonly GenericGetAllIncludeAsyncService<User> _userService = new();
    private readonly GenericGetAllIncludeAsyncService<Booking> _bookingService = new();
    
    public async Task ExportToJsonAsync(string path)
    {
        var rooms = (await _roomService.GetAllAsync(
                r => r.RoomCategory,
                r => r.RoomStatus))
            .Select(r => r.ToExport());
        var users = (await _userService.GetAllAsync(
                r => r.Role))
            .Select(u => u.ToExport());
        var bookings = (await _bookingService.GetAllAsync(
                b => b.Room,
                b => b.Client))
            .Select(b => b.ToExport());
        
        ExportData allData = new()
        {
            Rooms = rooms,
            RoomCategories = (await _roomCategoryService.GetAllAsync()).Select(c => c.ToExport()),
            RoomStatuses = (await _roomStatusService.GetAllAsync()).Select(s => s.ToExport()),
            Users = users,
            Roles = (await _roleService.GetAllAsync()).Select(r => r.ToExport()),
            Clients = (await _clientService.GetAllAsync()).Select(c=>c.ToExport()),
            Bookings = bookings,
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
        await ImportEntitiesAsync(
            data.RoomStatuses,
            async s => await context.RoomStatuses.AnyAsync(x=>x.Name == s.Name),
            async s => await context.RoomStatuses.AddAsync(new RoomStatus {Name = s.Name})
        );

        await ImportEntitiesAsync(
            data.RoomCategories,
            async c => await context.RoomCategories.AnyAsync(x => x.Name == c.Name),
            async c => await context.RoomCategories.AddAsync(new RoomCategory { Name = c.Name } )
        );
        
        await context.SaveChangesAsync();

        await ImportEntitiesAsync(
            data.Rooms,
            async r => await context.Rooms.AnyAsync(x=> x.Floor == r.Floor && x.Number == r.Number),
            async r =>
            {
                var category = await context.RoomCategories
                    .FirstOrDefaultAsync(c=>c.Name == r.RoomCategoryName);
                var status = await context.RoomStatuses
                    .FirstOrDefaultAsync(s => s.Name == r.RoomStatusName);
                if (category == null || status == null) 
                    throw new Exception("Ошибка при импорте Комнат:\n" +
                                        $"Категория с именем {r.RoomCategoryName} или Статус с именем {r.RoomStatusName}" +
                                        "не существует в БД");
                await context.Rooms.AddAsync(new Room
                {
                    Floor = r.Floor,
                    Number = r.Number,
                    RoomCategoryId = category.Id,
                    RoomStatusId = status.Id,
                });
            }
        );

        await ImportEntitiesAsync(
            data.Roles,
            async r => await context.Roles.AnyAsync(x=> x.Name == r.Name),
            async r => await  context.Roles.AddAsync(new Role { Name = r.Name } )
        );

        await context.SaveChangesAsync();
        
        await ImportEntitiesAsync(
            data.Users,
            async u => await context.Users.AnyAsync(x=> 
                x.Login == u.Login &&
                x.Password == u.Password),
            async u =>
            {
                var role = await context.Roles
                    .FirstOrDefaultAsync(r => r.Name == u.RoleName);
                if (role == null)
                    throw new Exception("Ошибка при импорте Пользователей:\n" +
                                        $"Роль с именем {u.RoleName} не существует в БД");
                await context.Users.AddAsync(new User 
                    { 
                        Login = u.Login,
                        Password = u.Password,
                        RoleId = role.Id,
                    }
                );
            }
        );

        await ImportEntitiesAsync(
            data.Clients,
            async c => await context.Clients.AnyAsync(x => x.CreatedAt == c.CreatedAt),
            async c => await context.Clients.AddAsync(c)
        );
        
        await context.SaveChangesAsync();
        
        await ImportEntitiesAsync(
            data.Bookings,
            async b => await context.Bookings.AnyAsync(x => x.EnterDate == b.EnterDate),
            async b =>
            {
                var client = await context.Clients
                    .FirstOrDefaultAsync(c => c.CreatedAt == b.ClientCreatedAt);
                var room = await context.Rooms
                    .FirstOrDefaultAsync(r => r.Floor == b.RoomFloor && r.Number == b.RoomNumber);

                if (client == null || room == null)
                    throw new Exception("Ошибка при импорте Бронирований:\n" +
                                        $"В БД не был найден Клиент, созданный {b.ClientCreatedAt} или\n" +
                                        $"Не была найдена Комната с номером {b.RoomNumber} и {b.RoomFloor} этажом");
                await context.Bookings.AddAsync(new Booking
                {
                    EnterDate = b.EnterDate,
                    ExitDate = b.ExitDate,
                    ClientId = client.Id,
                    RoomId = room.Id,
                });
            }
        );

        await context.SaveChangesAsync();
    }

    private async Task ImportEntitiesAsync<T>(
        IEnumerable<T> items,
        Func<T, Task<bool>> existAsync,
        Func<T, Task> addAsync)
    {
        foreach (var item in items)
        {
            if (await existAsync(item)) continue;
            
            await addAsync(item);
        }
    }
}