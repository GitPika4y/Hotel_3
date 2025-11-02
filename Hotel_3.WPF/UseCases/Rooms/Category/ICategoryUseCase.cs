using Hotel_3.Domain.Models;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Rooms.Category;

public interface ICategoryUseCase
{
    Task<Resource<RoomCategory?>> AddAsync(RoomCategory entity);
    Task<Resource<IEnumerable<RoomCategory>>> GetAllAsync();
    Task<Resource<RoomCategory?>> UpdateAsync(RoomCategory entity);
}