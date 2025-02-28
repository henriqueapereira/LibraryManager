using LibraryManager.API.Entities;
using LibraryManager.API.Enums;

namespace LibraryManager.API.Models;

public class CreateLoanInputModel
{
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    public Loan ToEntity() => new(IdUser, IdBook);
}
