using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Persistence;

namespace LibraryManager.Application.Services.Book;
public class BookService : IBookService
{
    private readonly LibraryManagerDbContext _context;

    public BookService(LibraryManagerDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<BookViewModel>> GetAll()
    {
        var books = _context.Books.ToList();

        if (books is null)
        {
            return ResultViewModel<List<BookViewModel>>.Error("Não existem livros cadastrados");
        }

        var model = books.Select(BookViewModel.FromEntity).ToList();

        return ResultViewModel<List<BookViewModel>>.Success(model);
    }

    public ResultViewModel<BookViewModel> GetById(int id)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book is null)
        {
            return ResultViewModel<BookViewModel>.Error("O livro não foi encontrado");
        }

        var model = BookViewModel.FromEntity(book);

        return ResultViewModel<BookViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateBookInputModel model)
    {
        var book = model.ToEntity();

        _context.Add(book);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(book.Id);
    }

    public ResultViewModel Update(int id, UpdateBookInputModel model)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book == null)
        {
            return ResultViewModel<BookViewModel>.Error("O livro não foi encontrado");
        }

        book.Update(model.Title, model.Author, model.ISBN, model.Year);

        _context.Books.Update(book);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }
    public ResultViewModel Delete(int id)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book == null)
        {
            return ResultViewModel<BookViewModel>.Error("O livro não foi encontrado");
        }

        _context.Books.Update(book);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }


}
