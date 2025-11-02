using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Rooms.Room;

public class RoomUseCase(IRoomService service) : IRoomUseCase
{
    public async Task<Resource<IEnumerable<Domain.Models.Room>>> GetRoomsAsync()
    {
        try
        {
            var rooms = await service.GetAllAsync(
                r => r.RoomCategory,
                r=> r.RoomStatus
            );
            return Resource<IEnumerable<Domain.Models.Room>>.Success(rooms);
        }
        catch (Exception e)
        {
            return Resource<IEnumerable<Domain.Models.Room>>.Fail(e.Message);
        }
    }

    public async Task<Resource<Domain.Models.Room?>> AddRoomAsync(Domain.Models.Room room)
    {
        try
        {
            var result = await service.AddAsync(room);
            return Resource<Domain.Models.Room?>.Success(result);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.Room?>.Fail(e.Message, e);
        }
    }

    public async Task<Resource<Domain.Models.Room?>> UpdateRoomAsync(Domain.Models.Room room)
    {
        try
        {
            var result = await service.UpdateAsync(room);
            return Resource<Domain.Models.Room?>.Success(result);
        }
        catch (Exception e)
        {
            return Resource<Domain.Models.Room?>.Fail(e.Message);
        }
    }
}