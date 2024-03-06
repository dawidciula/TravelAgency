using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;
namespace UbbRentalBike.Data;

public class RentalContext : DbContext
{
    public RentalContext(DbContextOptions<RentalContext> options) : base(options)
    {
    }
    
    public DbSet<Participants> Participant { get; set; }
    public DbSet<Reservations> Reservation { get; set; }
    public DbSet<Trips> Trip { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participants>().ToTable("Participants");
        modelBuilder.Entity<Reservations>().ToTable("Reservations");
        modelBuilder.Entity<Trips>().ToTable("Trips");
    }
}