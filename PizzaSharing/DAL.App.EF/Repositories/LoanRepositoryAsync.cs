using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.masirg.Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;

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

        public async Task<List<DALLoanTakenDTO>> AllUserTakenLoans(int appUserId)
        {
            var takenLoans = await RepoDbSet
                .Include(loan => loan.LoanGiver)
                .Include(loan => loan.LoanRows)
                    .ThenInclude(loanRow => loanRow.ReceiptRow)
                        .ThenInclude(receiptRow => receiptRow.Receipt)
                .Include(loan => loan.LoanRows)
                    .ThenInclude(loanRow => loanRow.ReceiptRow)
                        .ThenInclude(receiptRow => receiptRow.Product)
                            .ThenInclude(product => product.Prices)
                .Include(loan => loan.LoanRows)
                    .ThenInclude(loanRow => loanRow.ReceiptRow)
                        .ThenInclude(receiptRow => receiptRow.ReceiptRowChanges)
                            .ThenInclude(receiptRowChange => receiptRowChange.Change)
                                .ThenInclude(change => change.Prices)
                .Where(loan => loan.LoanTakerId == appUserId).ToListAsync();
            
            
            return takenLoans
                .Select(LoanTakenMapper.FromDomain)
                .Where(dto => dto.OwedAmount > decimal.Zero)
                .ToList();
        }

        public async Task<List<DALLoanGivenDTO>> AllUserGivenLoans(int appUserId)
        {
            var givenLoans = await RepoDbSet
                .Include(loan => loan.LoanTaker)
                .Include(loan => loan.LoanRows)
                .ThenInclude(loanRow => loanRow.ReceiptRow)
                .ThenInclude(receiptRow => receiptRow.Receipt)
                .Include(loan => loan.LoanRows)
                .ThenInclude(loanRow => loanRow.ReceiptRow)
                .ThenInclude(receiptRow => receiptRow.Product)
                .ThenInclude(product => product.Prices)
                .Include(loan => loan.LoanRows)
                .ThenInclude(loanRow => loanRow.ReceiptRow)
                .ThenInclude(receiptRow => receiptRow.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.Prices)
                .Where(loan => loan.LoanGiverId == appUserId).ToListAsync();

            return givenLoans
                .Select(LoanGivenMapper.FromDomain)
                .Where(dto => dto.OwedAmount > decimal.Zero)
                .ToList();
        }

        //ReceiptParticipant receipt must be referenced
        public async Task<Loan> FindOrAddAsync(ReceiptParticipant participant)
        {
            var loan = await RepoDbSet.FirstOrDefaultAsync(obj => obj.ReceiptParticipantId == participant.Id);
            if (loan != null) return loan;
            return (await RepoDbSet.AddAsync(new Loan()
            {
                ReceiptParticipantId = participant.Id,
                LoanTakerId = participant.AppUserId,
                LoanGiverId = participant.Receipt.ReceiptManagerId
            })).Entity;
        }
    }
}