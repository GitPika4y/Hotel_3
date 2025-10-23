using Hotel_3.Domain.DTOs;
using Hotel_3.Domain.Models;

namespace Hotel_3.Domain.Mappers;

public static class Mapper
{
    public static RoomDto ToDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Floor = room.Floor,
            Number = room.Number,
            Category = room.RoomCategory.Name,
            Status = room.RoomStatus.Name,
            CategoryId = room.RoomCategoryId,
            StatusId = room.RoomStatusId,
        };
    }

    public static Room ToExistModel(this RoomDto roomDto)
    {
        return new Room
        {
            Id = roomDto.Id,
            Floor = roomDto.Floor,
            Number = roomDto.Number,
            RoomCategoryId = roomDto.CategoryId,
            RoomStatusId = roomDto.StatusId,
        };
    }
    
    public static Room ToNewModel(this RoomDto roomDto)
    {
        return new Room
        {
            Floor = roomDto.Floor,
            Number = roomDto.Number,
            RoomCategoryId = roomDto.CategoryId,
            RoomStatusId = roomDto.StatusId,
        };
    }
    
    
}