using LibraryManager.API.Entities;
using LibraryManager.API.Models;
using LibraryManager.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly LibraryManagerDbContext _context;

    public LoansController(LibraryManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var loans = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book);
        var model = loans.Select(LoanViewModel.FromEntity).ToList();

        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        var loan = _context.Loans
            .Include(b => b.Book)
            .Include(b => b.User)
            .SingleOrDefault(l => l.Id == id);

        if (loan == null)
        {
            return BadRequest("Empréstimo não encontrado");
        }

        var model = LoanViewModel.FromEntity(loan);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(CreateLoanInputModel model)
    {
        var loan = model.ToEntity();
        
        loan.Lend();
        
        _context.Loans.Add(loan);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateLoanInputModel model) 
    {
        var loan = _context.Loans
            .Include(b => b.Book)
            .Include(u => u.User)
            .SingleOrDefault(l => l.Id == id);

        if (loan == null)
        {
            return BadRequest("Empréstimo não encontrado");
        }
        loan.Update(model.IdBook, model.IdUser);

        _context.Loans.Update(loan);
        _context.SaveChanges();

        return Ok("Empréstimo atualizado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) 
    {
        var loan = _context.Loans.SingleOrDefault(l => l.Id == id);

        if (loan == null)
        {
            return BadRequest("Empréstimo não encontrado");
        }

        _context.Loans.Remove(loan);
        _context.SaveChanges();

        return Ok("Empréstimo removido");
    }

    [HttpPut("{idUser}/return-book")]
    public IActionResult LoanReturned(int idUser, int idBook)
    {
        var loanReturn = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .SingleOrDefault(x => x.IdUser == idUser && x.IdBook == idBook);

        if (loanReturn == null)
        {
            throw new ArgumentException("Empréstimo não encontrado para o usuário ou livro informados");
        }

        if (loanReturn.ReturnDate < DateTime.Now)
        {
            throw new InvalidOperationException("O livro está atrasado.");
        }

        loanReturn.Returned();
        _context.Loans.Update(loanReturn);
        _context.SaveChanges();

        return Ok("O livro está em dia");
    }
}
