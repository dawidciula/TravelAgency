using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;
namespace UbbRentalBike.Data;

public class RentalContext : DbContext
{
    public RentalContext(DbContextOptions<RentalContext> options) : base(options)
    {
    }
    
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Trip> Trips { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>().ToTable("Participants");
        modelBuilder.Entity<Reservation>().ToTable("Reservations");
        modelBuilder.Entity<Trip>().ToTable("Trips");
    }
}