using Hotel_3.Domain.DTOs;
using Hotel_3.Domain.Models;

namespace Hotel_3.Domain.Mappers;

public static class Mapper
{
    public static Room ToNewModel(this Room room)
    {
        return new Room
        {
            Floor = room.Floor,
            Number = room.Number,
            RoomCategoryId = room.RoomCategoryId,
            RoomStatusId = room.RoomStatusId,
        };
    }
    
    public static RoomExport ToExport(this Room room)
    {
        return new RoomExport
        {
            Floor = room.Floor,
            Number = room.Number,
            RoomCategoryName = room.RoomCategory.Name,
            RoomStatusName = room.RoomStatus.Name,
        };
    }
    
    public static RoomCategory ToExport(this RoomCategory category)
    {
        return new RoomCategory
        {
            Name = category.Name,
        };
    }

    public static RoomStatus ToExport(this RoomStatus status)
    {
        return new RoomStatus
        {
            Name = status.Name,
        };
    }
}