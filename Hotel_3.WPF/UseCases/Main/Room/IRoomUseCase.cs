using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Main.Room;

public interface IRoomUseCase
{
    Task<Resource<IEnumerable<Domain.Models.Room>>> GetRoomsAsync();
}