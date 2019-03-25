using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LoanRepositoryAsync : BaseRepositoryAsync<Loan> , ILoanRepository
    {
        public LoanRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Loan>> AllAsync()
        {
            return await RepoDbSet
                .Include(loan => loan.LoanGiver)
                .Include(loan => loan.LoanTaker)
                .Include(loan => loan.ReceiptParticipant)
                .ToListAsync();
        }

        public override async Task<Loan> FindAsync(params object[] id)
        {
            var loan = await RepoDbSet.FindAsync(id);

            if (loan != null)
            {
                await RepoDbContext.Entry(loan).Reference(l => l.LoanGiver).LoadAsync();
                await RepoDbContext.Entry(loan).Reference(l => l.LoanTaker).LoadAsync();
                await RepoDbContext.Entry(loan).Reference(l => l.ReceiptParticipant).LoadAsync();
            }

            return loan;
        }
    }
}