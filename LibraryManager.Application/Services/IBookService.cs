using LibraryManager.Application.Models;

namespace LibraryManager.Application.Services;
public interface IBookService
{
    ResultViewModel<List<BookViewModel>> GetAll();
    ResultViewModel<BookViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateBookInputModel model);
    ResultViewModel Update(UpdateBookInputModel model);
    ResultViewModel Delete(int id);

}
