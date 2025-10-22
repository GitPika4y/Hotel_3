using Hotel_3.Domain.Services.Room;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Main.Room;

public class RoomUseCase(IRoomService service) : IRoomUseCase
{
    public async Task<Resource<IEnumerable<Domain.Models.Room>>> GetRoomsAsync()
    {
        var rooms = await service.GetAllAsync();
        return Resource<IEnumerable<Domain.Models.Room>>.Success(rooms);
    }
}