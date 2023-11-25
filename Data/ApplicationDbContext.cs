using Microsoft.EntityFrameworkCore;
using Urbaton.Models;

namespace Urbaton.Data;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(IConfiguration configuration) : base()
    {
        _connectionString = configuration.GetConnectionString("ParkingDB") ?? throw new ArgumentException("Connection string not found");
    }

    public DbSet<Parking> Parkings { get; set; }
    public DbSet<ParkingLot> ParkingLots { get; set; }
    public DbSet<Placemark> Placemarks { get; set; }
    public DbSet<Account> Accounts { get; internal set; }
    public DbSet<ParkingFeedback> ParkingFidback { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Placemark>().HasKey(x => new { x.Name, x.Description });
        modelBuilder.Entity<PlacemarkLookAt>().HasKey(x => new { x.Latitude, x.Longitude });
        modelBuilder.Entity<Account>().HasKey(x => x.DeviceId);
        modelBuilder.Entity<ParkingFeedback>().HasKey(x => new { x.ParkingId, x.UserId, x.Creation });
        base.OnModelCreating(modelBuilder);
    }
}