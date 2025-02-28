namespace LibraryManager.API.Entities;

public class User
{
    public User() { }
    public User(string fullName, string email, string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Loan> Loans { get; set; }
}
