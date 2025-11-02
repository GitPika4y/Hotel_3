namespace Hotel_3.Domain.Models
{
    public class Booking : EntityObject
    {
        public DateTime EnterDate { get; set; }
        public DateTime ExitDate { get; set; }
        
        public int RoomId { get; set; }
        public int ClientId { get; set; }
        
        public virtual Room Room { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
    }
}
