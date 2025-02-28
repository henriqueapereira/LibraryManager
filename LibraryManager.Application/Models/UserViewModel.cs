using LibraryManager.Core.Entities;

namespace LibraryManager.Application.Models;

public class UserViewModel
{
    public UserViewModel(int id, string fullName, string email, string password, List<string> loan)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Password = password;
        Loan = loan;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Loan { get; set; }

    public static UserViewModel FromEntity(User user)
    {
        var loans = user.Loans.Select(u => u.Book).Select(b => b.Title).ToList();

        return new(user.Id, user.FullName, user.Email, user.Password, loans);
    }

}
