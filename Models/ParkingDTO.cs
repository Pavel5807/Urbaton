namespace Urbatron.Models.DTOs;

public class ParkingDTO
{
    public Guid Id { get; set; }
    public ParkingType Type { get; set; }
    public bool SecurityCameras { get; set; }
    public double Workload { get; set; }
    public int Awailable { get; set; }
    public Placemark Placemark { get; set; }
}
