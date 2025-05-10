using LibraryManager.Application.Models;

namespace LibraryManager.Application.Services.User;
public interface IUserService
{
    ResultViewModel<List<UserViewModel>> GetAll();
    ResultViewModel<int> Insert(CreateUserInputModel model);
}
