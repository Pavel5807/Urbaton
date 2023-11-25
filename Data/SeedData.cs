using Urbaton.Data;
using Urbaton.Models;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        InitializeAccounts(context);
        InitializeParkings(context);
        InitializeParkingFidbacks(context);
    }

    private static void InitializeAccounts(ApplicationDbContext context)
    {
        if (context.Accounts.Any())
        {
            return;
        }

        context.Accounts.AddRange(
            new()
            {
                DeviceId = new Guid("00000000-0000-0000-0000-000000000001"),
                LotType = ParkingLotType.Personal
            },
            new()
            {
                DeviceId = new Guid("00000000-0000-0000-0000-000000000002"),
                LotType = ParkingLotType.Electric
            },
            new()
            {
                DeviceId = new Guid("00000000-0000-0000-0000-000000000003"),
                LotType = ParkingLotType.Truk
            }
        );

        context.SaveChanges();
    }

    private static void InitializeParkings(ApplicationDbContext context)
    {
        if (context.Parkings.Any())
        {
            return;
        }

        context.Parkings.AddRange(
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Lots = new List<ParkingLot>()
                {
                    new ()
                    {
                        AccessibleEnviroment = false,
                        BasePrice = 1,
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Status = ParkingLotStatus.Free,
                        Type = ParkingLotType.Personal
                    },
                    new ()
                    {
                        AccessibleEnviroment = false,
                        BasePrice = 0,
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Status = ParkingLotStatus.Booked,
                        Type = ParkingLotType.Electric
                    },
                    new ()
                    {
                        AccessibleEnviroment = true,
                        BasePrice = 1,
                        Id = new Guid("00000000-0000-0000-0000-000000000003"),
                        Status = ParkingLotStatus.Free,
                        Type = ParkingLotType.Personal
                    },
                },
                Placemark = new()
                {
                    Description = "Пн-Вс 8:00-18:00",
                    LookAt = new()
                    {
                        Altitude = 0,
                        Heading = 0,
                        Latitude = 30.0,
                        Longitude = 68.0,
                        Range = 0,
                        Tilt = 0
                    },
                    Name = "Парковка №1"
                },
                SecurityCameras = true,
                Type = ParkingType.Underground
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Lots = new List<ParkingLot>()
                {
                    new ()
                    {
                        AccessibleEnviroment = false,
                        BasePrice = 1,
                        Id = new Guid("00000000-0000-0000-0000-000000000004"),
                        Status = ParkingLotStatus.Free,
                        Type = ParkingLotType.Electric
                    },
                    new ()
                    {
                        AccessibleEnviroment = false,
                        BasePrice = 0,
                        Id = new Guid("00000000-0000-0000-0000-000000000005"),
                        Status = ParkingLotStatus.Booked,
                        Type = ParkingLotType.Electric
                    },
                    new ()
                    {
                        AccessibleEnviroment = true,
                        BasePrice = 1,
                        Id = new Guid("00000000-0000-0000-0000-000000000006"),
                        Status = ParkingLotStatus.Free,
                        Type = ParkingLotType.Personal
                    },
                },
                Placemark = new()
                {
                    Description = "Пн-Вс 8:00-18:00",
                    LookAt = new()
                    {
                        Altitude = 0,
                        Heading = 0,
                        Latitude = 32.0,
                        Longitude = 68.0,
                        Range = 0,
                        Tilt = 0
                    },
                    Name = "Парковка №2"
                },
                SecurityCameras = true,
                Type = ParkingType.Street
            }
        );

        context.SaveChanges();
    }

    private static void InitializeParkingFidbacks(ApplicationDbContext context)
    {
        if (context.ParkingFidback.Any())
        {
            return;
        }

        context.ParkingFidback.AddRange(
            new()
            {
                Body = "Отличная парковка",
                Creation = new DateTime(2023, 11, 25, 10, 03, 58).ToUniversalTime(),
                ParkingId = new Guid("00000000-0000-0000-0000-000000000001"),
                UserId = new Guid("00000000-0000-0000-0000-000000000001")
            },
            new()
            {
                Body = "Ужасный подъезд, вся дорога разбита",
                Creation = new DateTime(2023, 11, 25, 12, 10, 01).ToUniversalTime(),
                ParkingId = new Guid("00000000-0000-0000-0000-000000000002"),
                UserId = new Guid("00000000-0000-0000-0000-000000000001")
            }
        );

        context.SaveChanges();
    }
}