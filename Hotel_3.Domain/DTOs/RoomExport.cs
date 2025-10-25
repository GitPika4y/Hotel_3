namespace Hotel_3.Domain.DTOs;

public class RoomExport
{
    public int Floor { get; set; }
    public int Number { get; set; }
    public string RoomCategoryName { get; set; } = null!;
    public string RoomStatusName { get; set; } = null!;
}