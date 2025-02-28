using LibraryManager.Core.Enums;

namespace LibraryManager.Core.Entities;

public class Loan
{
    public Loan(int idUser, int idBook)
    {
        IdUser = idUser;
        IdBook = idBook;
        Status = LoanStatusEnum.Available;
    }

    public int Id { get; set; }
    public int IdUser { get; set; }
    public User User { get; set; }
    public int IdBook { get; set; }
    public Book Book { get; set; }
    public LoanStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ReturnDate { get; set; }

    public void Lend()
    {
        if (Status == LoanStatusEnum.Available)
        {
            Status = LoanStatusEnum.Borrowed;
            CreatedAt = DateTime.Now;
            ReturnDate = CreatedAt.AddDays(7);
        }
    }

    public void Returned()
    {
        if (Status == LoanStatusEnum.Borrowed)
        {
            Status = LoanStatusEnum.Returned;
        }
    }

    public void Unavailable()
    {
        if (Status == LoanStatusEnum.None || Status == LoanStatusEnum.Unavailable)
        {
            Status = LoanStatusEnum.Unavailable;
        }
    }

    public void Update(int idUser, int idBook)
    {
        IdUser = idUser;
        IdBook = idBook;
    }


}
