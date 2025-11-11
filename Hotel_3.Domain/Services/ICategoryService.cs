using Hotel_3.Domain.Models;
using Hotel_3.Domain.Services.Base;

namespace Hotel_3.Domain.Services;

public interface ICategoryService :
    IAddAsyncService<RoomCategory>,
    IGetAllAsyncService<RoomCategory>,
    IUpdateAsyncService<RoomCategory>;