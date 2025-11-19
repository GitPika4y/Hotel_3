using Hotel_3.WPF.Utils;

namespace Hotel_3.WPF.UseCases.Rooms.Booking;

public interface IBookingUseCase
{
    Task<Resource<Domain.Models.Booking?>> AddAsync(Domain.Models.Booking entity);
    Task<Resource<Domain.Models.Booking?>> UpdateAsync(Domain.Models.Booking entity, Domain.Models.Booking oldEntity);
    Task<Resource<IEnumerable<Domain.Models.Booking>>> GetAllAsync();
}