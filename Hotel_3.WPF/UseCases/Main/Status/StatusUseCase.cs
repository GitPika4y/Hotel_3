using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Main.Status;

public class StatusUseCase(IStatusService service) : IStatusUseCase
{
    public async Task<Resource<RoomStatus?>> AddAsync(RoomStatus entity)
    {
        try
        { 
            var status = await service.AddAsync(entity);
            return Resource<RoomStatus?>.Success(status);
        }
        catch (Exception e)
        {
            return  Resource<RoomStatus?>.Fail(e.Message);
        }
    }

    public async Task<Resource<RoomStatus?>> UpdateAsync(RoomStatus entity)
    {
        try
        { 
            var status = await service.UpdateAsync(entity);
            return Resource<RoomStatus?>.Success(status);
        }
        catch (Exception e)
        {
            return  Resource<RoomStatus?>.Fail(e.Message);
        }
    }

    public async Task<Resource<IEnumerable<RoomStatus>>> GetAllAsync()
    {
        try
        {
            var statuses = await service.GetAllAsync();
            return Resource<IEnumerable<RoomStatus>>.Success(statuses);
        }
        catch (Exception e)
        {
            return Resource<IEnumerable<RoomStatus>>.Fail(e.Message);
        }
    }
}