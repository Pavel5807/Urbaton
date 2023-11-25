namespace Urbaton.Models;

public class ParkingFeedback
{
    public Guid ParkingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime Creation { get; set; }
    public required string Body { get; set; }
}