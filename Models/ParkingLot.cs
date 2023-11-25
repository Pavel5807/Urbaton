namespace Urbaton.Models;

public class ParkingLot
{
    public Guid Id { get; set; }
    public ParkingLotStatus Status { get; set; }
    public ParkingLotType Type { get; set; }
    public bool Charging { get; set; }
    public bool AccessibleEnviroment { get; set; }
    public double BasePrice { get; set; }
}
