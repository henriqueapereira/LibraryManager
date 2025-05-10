using LibraryManager.Application.Models;
using LibraryManager.Application.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(CreateBookInputModel model)
    {
        var result = _service.Insert(model);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateBookInputModel model)
    {
        var result = _service.Update(id, model);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _service.Delete(id);

        return NoContent();
    }

}
