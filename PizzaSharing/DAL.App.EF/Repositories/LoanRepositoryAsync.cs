using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
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

        public async Task<List<LoanTakenDTO>> AllUserTakenLoans(int appUserId)
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
            
            var result = new List<LoanTakenDTO>(); 
            foreach (var loan in takenLoans)
            {
                var sum = decimal.Zero;
                foreach (var loanRow in loan.LoanRows)
                {
                    sum += loanRow.Involvement * loanRow.ReceiptRow.RowSumCost();
                }
                result.Add(new LoanTakenDTO()
                {
                    LoanGiverName = loan.LoanGiver.UserNickname,
                    LoanId = loan.Id,
                    OwedAmount = sum
                });
            }

            return result;
        }

        public async Task<List<LoanGivenDTO>> AllUserGivenLoans(int appUserId)
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
            
            var result = new List<LoanGivenDTO>(); 
            foreach (var loan in givenLoans)
            {
                var sum = decimal.Zero;
                foreach (var loanRow in loan.LoanRows)
                {
                    sum += loanRow.Involvement * loanRow.ReceiptRow.RowSumCost();
                }
                result.Add(new LoanGivenDTO()
                {
                    LoanTakerName = loan.LoanTaker.UserNickname,
                    LoanId = loan.Id,
                    OwedAmount = sum
                });
            }

            return result;
        }
    }
}