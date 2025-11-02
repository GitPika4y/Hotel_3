using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Rooms.Room;

public interface IRoomUseCase
{
    Task<Resource<IEnumerable<Domain.Models.Room>>> GetRoomsAsync();
    Task<Resource<Domain.Models.Room?>> AddRoomAsync(Domain.Models.Room room);
    Task<Resource<Domain.Models.Room?>> UpdateRoomAsync(Domain.Models.Room room);
}