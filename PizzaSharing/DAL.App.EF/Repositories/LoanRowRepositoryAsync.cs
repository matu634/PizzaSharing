using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;

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

        public async Task AddAsync(int loanId, int receiptRowId, decimal involvement)
        {
            var loanRow = new LoanRow()
            {
                Involvement = involvement,
                IsPaid = false,
                LoanId = loanId,
                ReceiptRowId = receiptRowId
            };

            await RepoDbSet.AddAsync(loanRow);
        }

        public async Task<DALLoanRowDTO> FindAsync(int loanRowId)
        {
            var loanRow = await RepoDbSet.FindAsync(loanRowId);
            if (loanRow == null) return null;
            
            await RepoDbContext.Entry(loanRow).Reference(l => l.Loan).LoadAsync();
            await RepoDbContext.Entry(loanRow.Loan).Reference(l => l.ReceiptParticipant).LoadAsync();
            await RepoDbContext.Entry(loanRow.Loan.ReceiptParticipant).Reference(l => l.Receipt).LoadAsync();

            return LoanRowMapper.FromDomain(loanRow);
        }

        public async Task<List<LoanRow>> FindByReceiptRow(int receiptRowId)
        {
            return await RepoDbSet.Where(loanRow => loanRow.ReceiptRowId == receiptRowId).ToListAsync();
        }
    }
}