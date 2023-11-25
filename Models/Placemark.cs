namespace Urbaton.Models;

public class Placemark
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PlacemarkLookAt LookAt { get; set; }
}
