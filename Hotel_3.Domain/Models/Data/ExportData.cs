using Hotel_3.Domain.DTOs;

namespace Hotel_3.Domain.Models.Data;

public class ExportData
{
    public IEnumerable<RoomExport> Rooms { get; set; } = null!;
    public IEnumerable<RoomCategory> RoomCategories { get; set; } = null!;
    public IEnumerable<RoomStatus> RoomStatuses { get; set; } = null!;
}