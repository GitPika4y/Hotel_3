namespace Hotel_3.Domain.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public int Number { get; set; }
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int CategoryId { get; set; }
    public int StatusId { get; set; }
}