using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LoanRepository : BaseRepository<Loan> , ILoanRepository
    {
        public LoanRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}