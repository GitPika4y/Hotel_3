namespace Hotel_3.Domain.Models
{
    public class User : EntityObject
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        
        public int RoleId { get; set; }
        
        public virtual Role Role { get; set; } = null!;
    }
}
