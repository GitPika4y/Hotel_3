using System.Linq.Expressions;
using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services;
using Hotel_3.EntityFramework.Services.Base;

namespace Hotel_3.EntityFramework.Services;

public class BookingService: IBookingService
{
    private readonly GenericAddAsyncService<Booking> _addService = new();
    private readonly GenericUpdateAsyncService<Booking> _updateService = new();
    private readonly GenericGetAllIncludeAsyncService<Booking> _getAllService = new();
    
    
    public async Task<Booking?> AddAsync(Booking entity)
    {
        return await _addService.AddAsync(entity);
    }

    public async Task<Booking?> UpdateAsync(Booking entity)
    {
        return await _updateService.UpdateAsync(entity);
    }
    
    public async Task<IEnumerable<Booking>> GetAllAsync(params Expression<Func<Booking, object>>[] includes)
    {
        return await _getAllService.GetAllAsync(includes);
    }

    public async Task<IEnumerable<Booking>> GetAllAsync(params string[] includes)
    {
        return await _getAllService.GetAllAsync(includes);
    }
}