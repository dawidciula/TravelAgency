using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;
namespace UbbRentalBike.Data;

public class RentalContext : DbContext
{
    private readonly IConfiguration _configuration;

    public RentalContext(DbContextOptions<RentalContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
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