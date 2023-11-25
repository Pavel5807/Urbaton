using Urbaton.Models;

namespace Urbaton.Repositories;

public interface IParkingRepository
{
    void Add(Account account);
    void Add(Parking parking);
    void Add(ParkingFeedback parking);
    void CloseBooking(Guid parkingId, Guid lotId);
    IEnumerable<Parking> Get();
    IEnumerable<Account> GetAccounts();
    IEnumerable<Parking> GetByFliter(ParkingLotType lotType, bool accessibleEnviroment);
    Parking? GetById(Guid parkingId);
    IEnumerable<ParkingFeedback> GetFeedbackByGuid(Guid parkingId);
    void OpenBooking(Guid parkingId, Guid lotId, Guid userId);
    void Save();
}
