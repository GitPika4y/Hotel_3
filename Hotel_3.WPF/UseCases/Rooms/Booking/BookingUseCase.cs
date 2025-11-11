using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Rooms.Booking;

public class BookingUseCase(IBookingService service, IStatusService statusService, IRoomService roomService): IBookingUseCase
{
    public async Task<Resource<Domain.Models.Booking?>> AddAsync(Domain.Models.Booking entity)
    {
        try
        {
            var booking = await service.AddAsync(entity);

            await SetRoomStatus(booking, "Занят");

            return Resource<Domain.Models.Booking?>.Success(booking);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.Booking?>.Fail("Ошибка при добавлении Бронирования", e); 
        }
    }

    public async Task<Resource<Domain.Models.Booking?>> UpdateAsync(Domain.Models.Booking entity)
    {
        try
        {
            var booking = await service.UpdateAsync(entity);
            return Resource<Domain.Models.Booking?>.Success(booking);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.Booking?>.Fail("Ошибка при обновлении Бронирования", e);
        }
    }

    public async Task<Resource<IEnumerable<Domain.Models.Booking>>> GetAllAsync()
    {
        try
        {
            var list = await service.GetAllAsync(
                "Room",
                "Client",
                "Room.RoomCategory",
                "Room.RoomStatus");
            return Resource<IEnumerable<Domain.Models.Booking>>.Success(list);
        }
        catch (Exception e)
        {
            return Resource<IEnumerable<Domain.Models.Booking>>.Fail("Ошибка при получении Бронирования", e);
        }
    }

    private async Task SetRoomStatus(Domain.Models.Booking? booking, string statusName)
    {
        if(booking == null)
            throw new Exception("Ошибка при добавлении Бронирования:" +
                                " Бронирование является null");

        var room = await roomService.GetByIdAsync(booking.RoomId);
        var busyStatus = (await statusService.GetAllAsync())
            .FirstOrDefault(s => s.Name==statusName);

        if (room == null || busyStatus == null) 
            throw new Exception("Ошибка при добавлении Бронирования:" +
                                $" Комната не была найдена или Статус '{statusName}' не был найден");
            
        room.RoomStatusId = busyStatus.Id;
        await roomService.UpdateAsync(room);
    }
}