namespace Urbatron.Models;

public class Parking
{
    public Guid Id { get; set; }
    public ParkingType Type { get; set; }
    public bool SecurityCameras { get; set; }
    public IEnumerable<ParkingLot> Lots { get; set; }
    public Placemark Placemark { get; set; }
}
