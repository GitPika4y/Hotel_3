namespace Hotel_3.Domain.DTOs;

public class BookingExport
{
    public DateTime EnterDate { get; set; }
    public DateTime ExitDate { get; set; }
    public int RoomFloor { get; set; }
    public int RoomNumber { get; set; }
    public DateTime ClientCreatedAt { get; set; }
}