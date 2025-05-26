using LibraryManager.Application.Models;
using LibraryManager.Application.Services.Loan;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly ILoanService _service;

    public LoansController(ILoanService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll()
    {
        var results = _service.GetAll();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(CreateLoanInputModel model)
    {
        var result = _service.Insert(model);

        if (!result.IsSuccess)
            return BadRequest(result); 

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateLoanInputModel model)
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

    [HttpPut("returned")]
    public IActionResult LoanReturned(int idLoan)
    {
        var result = _service.LoanReturned(idLoan);

        return NoContent();
    }
}
