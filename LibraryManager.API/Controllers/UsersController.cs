using LibraryManager.API.Models;
using LibraryManager.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly LibraryManagerDbContext _context;

    public UsersController(LibraryManagerDbContext context) 
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model) 
    {
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
    

}
