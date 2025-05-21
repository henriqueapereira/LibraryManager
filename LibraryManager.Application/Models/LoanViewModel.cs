using System.Globalization;
using System.Text.Json.Serialization;
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

    [JsonIgnore]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public DateTime ReturnDate { get; set; }
    public int IdBook { get; set; }
    public int UserId { get; set; }
    public LoanStatusEnum Status { get; set; }

    // Propriedades somente para exibição formatada
    [JsonIgnore]
    public string CreatedAtFormatted => CreatedAt.ToString("dd/MM/yyyy", new CultureInfo("pt-BR"));
    [JsonIgnore]
    public string ReturnDateFormatted => ReturnDate.ToString("dd/MM/yyyy", new CultureInfo("pt-BR"));

    // Alias para manter os nomes esperados no JSON
    [JsonPropertyName("createdAt")]
    public string CreatedAtString => CreatedAtFormatted;

    [JsonPropertyName("returnDate")]
    public string ReturnDateString => ReturnDateFormatted;
    
    public static LoanViewModel FromEntity(Loan loan) => new(loan.Id, loan.Book.Title, loan.User.FullName, loan.CreatedAt, loan.ReturnDate, loan.IdBook, loan.IdUser, loan.Status);
}
