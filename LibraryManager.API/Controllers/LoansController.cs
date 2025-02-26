using LibraryManager.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreateLoanInputModel model)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(UpdateLoanInputModel model) 
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) 
    {
        return Ok();
    }
}
