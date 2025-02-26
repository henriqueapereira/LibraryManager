using LibraryManager.API.Enums;

namespace LibraryManager.API.Entities;

public class Loan
{
    public Loan(int id, int idUser, int idBook)
    {
        Id = id;
        IdUser = idUser;
        IdBook = idBook;
    }

    public int Id { get; set; }
    public int IdUser { get; set; } 
    public User User { get; set; }
    public int IdBook { get; set; }
    public Book Book { get; set; }
    public LoanStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ReturnDate { get; set; }
}
