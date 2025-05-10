using LibraryManager.Application.Models;
using LibraryManager.Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model) 
    {
        var result = _service.Insert(model);

        return NoContent();
    }
}
