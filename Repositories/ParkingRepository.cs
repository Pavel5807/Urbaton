using Urbaton.Data;
using Urbaton.Models;

namespace Urbaton.Repositories;

public class ParkingRepository : IParkingRepository
{
    private readonly ApplicationDbContext _context;

    public ParkingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Parking parking)
    {
        _context.Add(parking);
    }

    public void CloseBooking(Guid parkingId, Guid lotId)
    {
        var parking = _context.Parkings.FirstOrDefault(x => x.Id == parkingId);
        var lot = parking?.Lots.FirstOrDefault(x => x.Id == lotId);

        if (lot is null)
        {
            return;
        }

        lot.Status = ParkingLotStatus.Free;
    }

    public IEnumerable<Parking> Get()
    {
        return _context.Parkings.ToList();
    }

    public Parking? GetById(Guid parkingId)
    {
        return _context.Parkings.FirstOrDefault(x => x.Id == parkingId);
    }

    public void OpenBooking(Guid parkingId, Guid lotId, Guid userId)
    {
        var parking = _context.Parkings.FirstOrDefault(x => x.Id == parkingId);
        var lot = parking?.Lots.FirstOrDefault(x => x.Id == lotId);

        if (lot is null)
        {
            return;
        }

        lot.Status = ParkingLotStatus.Booked;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
