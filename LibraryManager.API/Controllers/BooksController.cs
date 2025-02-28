using LibraryManager.API.Entities;
using LibraryManager.API.Models;
using LibraryManager.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryManagerDbContext _context;

    public BooksController(LibraryManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var books = _context.Books;

        if (books is null)
        {
            return BadRequest("Não existem livros cadastrados");
        }

        var model = books.Select(BookViewModel.FromEntity).ToList();

        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book is null)
        {
            return BadRequest("Não existem livros cadastrados");
        }

        var model = BookViewModel.FromEntity(book);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(CreateBookInputModel model)
    {
        var book = new Book(model.Title, model.Author, model.ISBN, model.Year);

        _context.Add(book);
        _context.SaveChanges();

        return Ok("Livro cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateBookInputModel model)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book == null)
        {
            return BadRequest("Livro não encontrado");
        }

        book.Update(model.Title, model.Author, model.ISBN, model.Year);

        _context.Books.Update(book);
        _context.SaveChanges();

        return Ok("Livro atualizado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book == null)
        {
            return BadRequest("Livro não encontrado");
        }

        _context.Books.Update(book);
        _context.SaveChanges();

        return Ok("Livro removido");
    }

}
