using LibraryManager.Application.Models;

namespace LibraryManager.Application.Services.Book;
public interface IBookService
{
    ResultViewModel<List<BookViewModel>> GetAll();
    ResultViewModel<BookViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateBookInputModel model);
    ResultViewModel Update(int id, UpdateBookInputModel model);
    ResultViewModel Delete(int id);

}
