using Urbatron.Models;

namespace Urbatron.Repositories;

public interface IParkingRepository
{
    void Add(Parking parking);
    void CloseBooking(Guid parkingId, Guid lotId);
    IEnumerable<Parking> Get();
    Parking? GetById(Guid parkingId);
    void OpenBooking(Guid parkingId, Guid lotId, Guid userId);
    void Save();
}
