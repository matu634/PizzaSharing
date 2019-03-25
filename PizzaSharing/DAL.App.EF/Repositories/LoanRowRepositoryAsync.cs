using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LoanRowRepositoryAsync : BaseRepositoryAsync<LoanRow>, ILoanRowRepository
    {
        public LoanRowRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<LoanRow>> AllAsync()
        {
            return await RepoDbSet
                .Include(loanRow => loanRow.ReceiptRow)
                .Include(loanRow => loanRow.Loan)
                .ToListAsync();
        }

        public override async Task<LoanRow> FindAsync(params object[] id)
        {
            var loanRow = await RepoDbSet.FindAsync(id);

            if (loanRow != null)
            {
                await RepoDbContext.Entry(loanRow).Reference(l => l.ReceiptRow).LoadAsync();
                await RepoDbContext.Entry(loanRow).Reference(l => l.Loan).LoadAsync();
            }

            return loanRow;
        }
    }
}