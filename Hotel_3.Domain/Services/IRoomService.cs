using Hotel_3.Domain.Services.Base;

namespace Hotel_3.Domain.Services;

public interface IRoomService :
    IAddAsyncService<Models.Room>,
    IUpdateAsyncService<Models.Room>,
    IGetAllAsyncService<Models.Room>
{
    
}
