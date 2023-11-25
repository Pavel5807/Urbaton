using Microsoft.AspNetCore.Mvc;
using Urbaton.Repositories;

namespace Urbaton.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class AccountController : ControllerBase
{
    private readonly IParkingRepository _repository;

    public AccountController(IParkingRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        _repository.Add(account);
        _repository.Save();

        return Ok();
    }

}
