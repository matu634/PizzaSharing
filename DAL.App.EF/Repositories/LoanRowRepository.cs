using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LoanRowRepository : BaseRepository<LoanRow>, ILoanRowRepository
    {
        public LoanRowRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}