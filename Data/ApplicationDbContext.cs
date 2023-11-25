using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Urbaton.Models;

namespace Urbaton.Data;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(IConfiguration configuration) : base()
    {
        _connectionString = configuration.GetConnectionString("ParkingDB") ?? throw new ArgumentException("Connection string not found");
    }

    public DbSet<Account> Accounts { get; internal set; }
    public DbSet<Parking> Parkings { get; set; }
    public DbSet<ParkingLot> ParkingLots { get; set; }
    public DbSet<Placemark> Placemarks { get; set; }
    public DbSet<ParkingFeedback> ParkingFidback { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entityBuilder =>
        {
            entityBuilder.HasKey(x => x.DeviceId);
        });

        modelBuilder.Entity<Parking>(entityBuilder =>
        {
            entityBuilder.HasKey(parking => parking.Id);
            entityBuilder.Property(parking => parking.Type)
                .HasConversion(new EnumToStringConverter<ParkingType>());
        });

        modelBuilder.Entity<ParkingFeedback>(entityBuilder =>
        {
            entityBuilder.HasKey(x => new
            {
                x.ParkingId,
                x.UserId,
                x.Creation
            });
        });

        modelBuilder.Entity<ParkingLot>(entityBuilder =>
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<ParkingLotStatus>());
            entityBuilder.Property(x => x.Type)
                .HasConversion(new EnumToStringConverter<ParkingLotType>());
        });

        modelBuilder.Entity<Placemark>(entityBuilder =>
        {
            entityBuilder.HasKey(x => new
            {
                x.Name,
                x.Description
            });
        });

        modelBuilder.Entity<PlacemarkLookAt>(entityBuilder =>
        {
            entityBuilder.HasKey(x => new
            {
                x.Latitude,
                x.Longitude
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}