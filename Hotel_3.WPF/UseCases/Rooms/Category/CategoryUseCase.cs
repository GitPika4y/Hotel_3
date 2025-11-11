using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Rooms.Category;

public class CategoryUseCase(ICategoryService service) : ICategoryUseCase
{
    public async Task<Resource<RoomCategory?>> AddAsync(RoomCategory entity)
    {
        try
        {
            var newEntity = await service.AddAsync(entity);
            return Resource<RoomCategory?>.Success(newEntity);
        }
        catch (Exception e)
        {
            return Resource<RoomCategory?>.Fail(e.Message);
        }
    }

    public async Task<Resource<IEnumerable<RoomCategory>>> GetAllAsync()
    {
        var list = await service.GetAllAsync();
        return Resource<IEnumerable<RoomCategory>>.Success(list);
    }

    public async Task<Resource<RoomCategory?>> UpdateAsync(RoomCategory entity)
    {
        try
        {
            var updatedEntity = await service.UpdateAsync(entity);
            return Resource<RoomCategory?>.Success(updatedEntity);
        }
        catch (Exception e)
        {
            return Resource<RoomCategory?>.Fail(e.Message);
        }
    }
}