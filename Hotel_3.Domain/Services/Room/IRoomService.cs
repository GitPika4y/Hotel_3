using Hotel_3.Domain.Services.Base;

namespace Hotel_3.Domain.Services.Room;

public interface IRoomService :
    IAddAsyncService<Models.Room>,
    IUpdateAsyncService<Models.Room>,
    IGetByIdAsyncService<Models.Room>,
    IGetAllAsyncService<Models.Room>
{
    
}
