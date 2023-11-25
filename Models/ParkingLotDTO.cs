using Urbatron.Models;

public class ParkingLotDTO
{
    public Guid Id { get; set; }
    public ParkingLotStatus Status { get; set; }
    public ParkingLotType Type { get; set; }
    public bool Charging { get; set; }
    public bool AccessibleEnvironment { get; set; }
    public double Price { get; set; }
}
