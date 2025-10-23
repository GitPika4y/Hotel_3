using Hotel_3.Domain.Models;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Main.Status;

public interface IStatusUseCase
{
    Task<Resource<RoomStatus?>> AddAsync(RoomStatus entity);
    Task<Resource<RoomStatus?>> UpdateAsync(RoomStatus entity);
    Task<Resource<IEnumerable<RoomStatus>>> GetAllAsync(); 
}