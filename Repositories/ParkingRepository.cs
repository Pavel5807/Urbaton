using Microsoft.EntityFrameworkCore;
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
        _context.Parkings.Add(parking);
    }

    public void Add(Account account)
    {
        _context.Accounts.Add(account);
    }

    public void Add(ParkingFeedback feedback)
    {
        _context.ParkingFidback.Add(feedback);
    }

    public void CloseBooking(Guid parkingId, Guid lotId)
    {
        var parking = _context.Parkings
            .Include(x => x.Lots)
            .Include(x => x.Placemark)
            .FirstOrDefault(x => x.Id == parkingId);
        var lot = parking?.Lots.FirstOrDefault(x => x.Id == lotId);

        if (lot is null)
        {
            return;
        }

        lot.Status = ParkingLotStatus.Free;
    }

    public IEnumerable<Parking> Get()
    {
        return _context.Parkings
            .Include(x => x.Lots)
            .Include(x => x.Placemark)
            .ToList();
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _context.Accounts.ToList();
    }

    public IEnumerable<Parking> GetByFliter(ParkingLotType lotType, bool accessibleEnviroment)
    {
        return _context.Parkings
            .Include(x => x.Lots)
            .Include(x => x.Placemark)
            .Where(x =>
                x.Lots.Any(l => l.Type == lotType
                    && l.AccessibleEnviroment == accessibleEnviroment
                    && l.Status == ParkingLotStatus.Free))
            .ToList();
    }

    public Parking? GetById(Guid parkingId)
    {
        return _context.Parkings
            .Include(x => x.Lots)
            .Include(x => x.Placemark)
            .FirstOrDefault(x => x.Id == parkingId);
    }

    public IEnumerable<Parking> GetByLocation(int lantitude, int longitude)
    {
        return _context.Parkings
            .Include(x => x.Lots)
            .Include(x => x.Placemark)
            .ThenInclude(x => x.LookAt)
            .OrderBy(x => Math.Pow(x.Placemark.LookAt.Latitude - lantitude, 2) + Math.Pow(x.Placemark.LookAt.Longitude - longitude, 2))
            .Take(10)
            .ToList();
    }

    public IEnumerable<ParkingFeedback> GetFeedbackByGuid(Guid parkingId)
    {
        return _context.ParkingFidback
            .Where(x => x.ParkingId == parkingId)
            .ToList();
    }

    public void OpenBooking(Guid parkingId, Guid lotId, Guid userId)
    {
        var parking = _context.Parkings
            .Include(x => x.Lots)
            .Include(x => x.Placemark)
            .FirstOrDefault(x => x.Id == parkingId);
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
