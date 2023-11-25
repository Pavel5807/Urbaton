using Microsoft.AspNetCore.Mvc;
using Urbaton.Models;
using Urbaton.Models.DTOs;
using Urbaton.Repositories;

namespace Urbaton.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ParkingController : ControllerBase
{
    private readonly IParkingRepository _repository;

    public ParkingController(IParkingRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetParkings()
    {
        var parkings = _repository.Get();

        return Ok(ParkingsToDTOs(parkings));
    }

    [HttpGet]
    public IActionResult GetParkingsByFilter([FromQuery] ParkingLotType lotType, [FromQuery] bool accessibleEnviroment)
    {
        var parkings = _repository.GetByFliter(lotType, accessibleEnviroment);

        return Ok(ParkingsToDTOs(parkings));
    }

    [HttpGet("{id}/lots")]
    public IActionResult GetLotsByParkingId(Guid parkingId)
    {
        var parking = _repository.GetById(parkingId);

        if (parking is null)
        {
            return BadRequest("Parking not found");
        }

        return Ok(ParkingLotsToDTOs(parking.Lots));
    }

    [HttpPost("/booking/open")]
    public IActionResult OpenBookingParkingLot([FromQuery] Guid userId, [FromQuery] Guid parkingId, [FromQuery] Guid lotId)
    {
        _repository.OpenBooking(parkingId, lotId, userId);
        _repository.Save();

        return Ok();
    }

    [HttpPost("/booking/close")]
    public IActionResult CloseBookingParkingLot([FromQuery] Guid parkingId, [FromQuery] Guid lotId)
    {
        _repository.CloseBooking(parkingId, lotId);
        _repository.Save();

        return Ok();
    }

    [HttpPost]
    public IActionResult Create(Parking parking)
    {
        _repository.Add(parking);
        _repository.Save();
        return Ok();
    }

    [HttpPost("/feedback")]
    public IActionResult CreateFeedback(ParkingFeedback feedback)
    {
        _repository.Add(feedback);
        return Ok();
    }

    [HttpGet("/feedback")]
    public IActionResult GetFeedback(Guid parkingId)
    {
        _repository.GetFeedbackByGuid(parkingId);

        return Ok();
    }

    private static IEnumerable<ParkingDTO> ParkingsToDTOs(IEnumerable<Parking> parkings)
    {
        return parkings.Select(p => new ParkingDTO()
        {
            Awailable = p.Lots.Count(x => x.Status is ParkingLotStatus.Free),
            Id = p.Id,
            Placemark = p.Placemark,
            SecurityCameras = p.SecurityCameras,
            Type = p.Type,
            Workload = p.Lots.Count(x => x.Status is ParkingLotStatus.Booked) / p.Lots.Count()
        });
    }

    private static IEnumerable<ParkingLotDTO> ParkingLotsToDTOs(IEnumerable<ParkingLot> lots)
    {
        var workload = lots.Count(x => x.Status is ParkingLotStatus.Booked) / lots.Count();

        var k = 1d;
        if (workload < 0.2)
        {
            k = 0.8;
        }
        else if (workload > 0.8)
        {
            k = 1.2;
        }

        return lots.Where(l => l.Status is ParkingLotStatus.Free).Select(l => new ParkingLotDTO()
        {
            AccessibleEnvironment = l.AccessibleEnviroment,
            Id = l.Id,
            Price = k * l.BasePrice,
            Status = l.Status,
            Type = l.Type
        });
    }
}
