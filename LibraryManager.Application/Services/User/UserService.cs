using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Services.User;
public class UserService : IUserService
{
    private readonly LibraryManagerDbContext _context;

    public UserService(LibraryManagerDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<UserViewModel>> GetAll()
    {
        var user = _context.Users
            .Include(u => u.Loans)
                .ThenInclude(b => b.Book)
            .ToList();

        if (user is null)
        {
            ResultViewModel<List<UserViewModel>>.Error("Não existe usuários cadastrados");
        }

        var model = user.Select(UserViewModel.FromEntity).ToList();

        return ResultViewModel<List<UserViewModel>>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateUserInputModel model)
    {
        var user = model.ToEntity();

        _context.Add(user);
        _context.SaveChangesAsync();

        return ResultViewModel<int>.Success(user.Id);
    }
}
