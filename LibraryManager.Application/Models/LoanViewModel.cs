using LibraryManager.Core.Entities;
using LibraryManager.Core.Enums;

namespace LibraryManager.Application.Models;

public class LoanViewModel
{
    public LoanViewModel(int id, string title, string fullName, DateTime createdAt, DateTime returnDate, int idBook, int userId, LoanStatusEnum status)
    {
        Id = id;
        Title = title;
        FullName = fullName;
        CreatedAt = createdAt;
        ReturnDate = returnDate;
        IdBook = idBook;
        UserId = userId;
        Status = status;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ReturnDate { get; set; }
    public int IdBook { get; set; }
    public int UserId { get; set; }
    public LoanStatusEnum Status { get; set; }

    public static LoanViewModel FromEntity(Loan loan) => new(loan.Id, loan.Book.Title, loan.User.FullName, loan.CreatedAt, loan.ReturnDate, loan.IdBook, loan.IdUser, loan.Status);
}
