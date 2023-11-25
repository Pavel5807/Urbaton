using Urbaton.Models;

namespace Urbaton.Repositories;

public interface IParkingRepository
{
    void Add(Account account);
    void Add(Parking parking);
    void Add(ParkingFeedback parking);
    void CloseBooking(Guid parkingId, Guid lotId);
    IEnumerable<Parking> Get();
    IEnumerable<Parking> GetByFliter(ParkingLotType lotType, bool charging, bool accessibleEnviroment);
    Parking? GetById(Guid parkingId);
    void GetFeedbackByGuid(Guid parkingId);
    void OpenBooking(Guid parkingId, Guid lotId, Guid userId);
    void Save();
}
