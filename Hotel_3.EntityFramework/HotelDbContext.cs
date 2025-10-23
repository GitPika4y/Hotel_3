using Hotel_3.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_3.EntityFramework;

public class HotelDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<RoomStatus> RoomStatuses { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomCategory> RoomCategories { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}