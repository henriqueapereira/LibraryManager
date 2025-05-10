using LibraryManager.Core.Entities;

namespace LibraryManager.Application.Models;

public class CreateUserInputModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User ToEntity()
        => new(FullName, Email, Password);
}
