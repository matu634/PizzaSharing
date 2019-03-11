using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LoanRowRepositoryAsync : BaseRepositoryAsync<LoanRow>, ILoanRowRepository
    {
        public LoanRowRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }
}