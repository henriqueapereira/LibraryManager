using LibraryManager.Application.Models;
using LibraryManager.Core.Entities;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var users = _context.Users
            .Include(u => u.Loans)
                .ThenInclude(b => b.Book)
            .ToList();

        var model = users.Select(UserViewModel.FromEntity).ToList();

        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model) 
    {
        var user = new User(model.FullName, model.Email, model.Password);

        _context.Add(user);
        _context.SaveChanges();

        return Ok("Usuario cadastrado"); 
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        _context.Remove(user);
        _context.SaveChanges();

        return Ok("Usuario removido");
    }
    

}
