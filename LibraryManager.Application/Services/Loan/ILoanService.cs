using LibraryManager.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Services.Loan;
public interface ILoanService
{
    ResultViewModel<List<LoanViewModel>> GetAll();
    ResultViewModel<LoanViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateLoanInputModel model);
    ResultViewModel Update(int id, UpdateLoanInputModel model);
    ResultViewModel Delete(int id);
    ResultViewModel LoanReturned(int idLoan);
}
