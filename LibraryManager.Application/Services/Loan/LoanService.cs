using LibraryManager.Application.Models;
using LibraryManager.Core.Enums;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Services.Loan;

public class LoanService : ILoanService
{
    private readonly LibraryManagerDbContext _context;

    public LoanService(LibraryManagerDbContext context)
    {
        _context = context;
    }
    public ResultViewModel<List<LoanViewModel>> GetAll()
{
    var today = DateTime.Now.Date;

    var loans = _context.Loans
        .Include(u => u.User)
        .Include(b => b.Book)
        .Where(l => true) // Só para manter a cadeia fluente
        .ToList();

    bool updated = false;

    foreach (var loan in loans)
    {
        if (loan.Status != LoanStatusEnum.Late && loan.Status != LoanStatusEnum.Returned && loan.ReturnDate.Date < today)
        {
            loan.Status = LoanStatusEnum.Late;
            updated = true;
        }
    }

    if (updated)
    {
        _context.SaveChanges();
    }

    var model = loans.Select(LoanViewModel.FromEntity).ToList();

    return ResultViewModel<List<LoanViewModel>>.Success(model);
}


    /*
    public ResultViewModel<List<LoanViewModel>> GetAll()
    {
        var loans = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .ToList();

        bool updated = false;
        var today = DateTime.Now.Date;

        foreach (var loan in loans)
        {
            if (loan.Status != Core.Enums.LoanStatusEnum.Late && loan.ReturnDate.Date < today)
            {
                loan.Status = LoanStatusEnum.Late;
                updated = true;
            }
        }

        if (updated)
        {
            _context.SaveChanges();
        }

        var model = loans.Select(LoanViewModel.FromEntity).ToList();

        return ResultViewModel<List<LoanViewModel>>.Success(model);
    }
    */
    public ResultViewModel<LoanViewModel> GetById(int id)
    {
        var loan = _context.Loans
            .Include(b => b.Book)
            .Include(b => b.User)
            .SingleOrDefault(l => l.Id == id);


        if (loan == null)
        {
            return ResultViewModel<LoanViewModel>.Error("Empréstimo não encontrado");
        }

        var model = LoanViewModel.FromEntity(loan);

        return ResultViewModel<LoanViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateLoanInputModel model)
    {
        var loan = model.ToEntity();

        loan.Lend();

        _context.Loans.Add(loan);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(loan.Id);
    }


    public ResultViewModel Update(int id, UpdateLoanInputModel model)
    {
        var loan = _context.Loans
            .Include(b => b.Book)
            .Include(u => u.User)
            .SingleOrDefault(l => l.Id == id);

        if (loan == null)
        {
            return ResultViewModel.Error("Empréstimo não encontrado");
        }
        loan.Update(model.IdBook, model.IdUser);

        _context.Loans.Update(loan);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }
    public ResultViewModel Delete(int id)
    {
        var loan = _context.Loans.SingleOrDefault(l => l.Id == id);

        if (loan == null)
        {
            return ResultViewModel.Error("Empréstimo não encontrado");
        }

        _context.Loans.Remove(loan);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    /*
    public ResultViewModel LoanReturned(int idUser, int idBook)
    {
        var loanReturn = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .SingleOrDefault(x => x.IdUser == idUser && x.IdBook == idBook);

        if (loanReturn == null)
        {
            throw new ArgumentException("Empréstimo não encontrado para o usuário ou livro informados");
        }

        Console.WriteLine("Status antes: " + loanReturn.Status);
        loanReturn.Returned();
        Console.WriteLine("Status depois: " + loanReturn.Status);

        _context.Loans.Update(loanReturn);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }
    */
    
    public ResultViewModel LoanReturned(int idLoan)
{
    var loanReturn = _context.Loans
        .Include(u => u.User)
        .Include(b => b.Book)
        .SingleOrDefault(x => x.Id == idLoan);

    if (loanReturn == null)
    {
        return ResultViewModel.Error("Empréstimo não encontrado para o ID informado.");
    }

    loanReturn.Returned(); // Altera o status para Returned, se válido

    _context.Loans.Update(loanReturn);
    _context.SaveChanges();

    return ResultViewModel.Success(); 
}
}
