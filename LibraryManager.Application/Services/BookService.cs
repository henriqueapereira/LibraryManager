using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Persistence;

namespace LibraryManager.Application.Services;
public class BookService : IBookService
{
    private readonly LibraryManagerDbContext _context;

    public BookService(LibraryManagerDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<BookViewModel>> GetAll()
    {
        var books = _context.Books;

        var model = books.Select(BookViewModel.FromEntity).ToList();

        return ResultViewModel<List<BookViewModel>>.Success(model);
    }

    public ResultViewModel<BookViewModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public ResultViewModel<int> Insert(CreateBookInputModel model)
    {
        throw new NotImplementedException();
    }

    public ResultViewModel Update(UpdateBookInputModel model)
    {
        throw new NotImplementedException();
    }
    public ResultViewModel Delete(int id)
    {
        throw new NotImplementedException();
    }
}
