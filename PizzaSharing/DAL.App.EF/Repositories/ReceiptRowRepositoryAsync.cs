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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PublicApi.DTO;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRowRepositoryAsync : BaseRepositoryAsync<ReceiptRow>, IReceiptRowRepository
    {
        public ReceiptRowRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<ReceiptRow>> AllAsync()
        {
            return await RepoDbSet
                .Include(row => row.Product)
                .Include(row => row.Receipt)
                .ToListAsync();
        }

        public override async Task<ReceiptRow> FindAsync(params object[] id)
        {
            var receiptRow = await RepoDbSet.FindAsync(id);

            if (receiptRow != null)
            {
                await RepoDbContext.Entry(receiptRow).Reference(row => row.Product).LoadAsync();
                await RepoDbContext.Entry(receiptRow).Reference(row => row.Receipt).LoadAsync();
            }

            return receiptRow;
        }

        public async Task<List<ReceiptRowAllDTO>> AllReceiptsRows(int receiptId, DateTime time)
        {
            var rows = await RepoDbSet
                .Include(row => row.Product)
                .ThenInclude(product => product.Prices)
                .Include(row => row.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.Prices)
                .Include(row => row.RowParticpantLoanRows)
                .ThenInclude(row => row.Loan)
                .ThenInclude(loan => loan.LoanTaker)
                .Where(row => row.ReceiptId == receiptId)
                .ToListAsync();
            var result = new List<ReceiptRowAllDTO>();
            foreach (var row in rows)
            {
                var product = new ProductDTO()
                {
                    ProductId = row.ProductId,
                    ProductName = row.Product.ProductName,
                    ProductPrice = row.Product.GetPriceAtTime(time)
                };
                var changes = new List<ChangeDTO>();
                foreach (var rowChange in row.ReceiptRowChanges)
                {
                    changes.Add(new ChangeDTO()
                    {
                        Name = rowChange.Change.ChangeName,
                        Price = rowChange.Change.GetPriceAtTime(time),
                        ChangeId = rowChange.ChangeId,
                        OrganizationId = rowChange.Change.OrganizationId,
                        ReceiptRowId = rowChange.ReceiptRowId
                    });
                }

                var participants = new List<RowParticipantDTO>();
                foreach (var loanRow in row.RowParticpantLoanRows)
                {
                    participants.Add(new RowParticipantDTO()
                    {
                        Name = loanRow.Loan.LoanTaker.UserNickname,
                        Involvement = loanRow.Involvement,
                        ReceiptRowId = row.Id,
                        LoanId = loanRow.LoanId,
                        AppUserId = loanRow.Loan.LoanTakerId,
                        LoanRowId = loanRow.Id
                    });
                }

                result.Add(new ReceiptRowAllDTO()
                {
                    Amount = row.Amount,
                    Product = product,
                    Changes = changes,
                    Discount = row.RowDiscount,
                    ReceiptId = receiptId,
                    ReceiptRowId = row.Id,
                    CurrentCost = row.RowSumCost(),
                    Participants = participants
                });
            }

            return result;
        }

        public async Task<ReceiptRow> AddAsync(DALReceiptRowMinDTO rowMin)
        {
            var receiptRow = new ReceiptRow()
            {
                Amount = rowMin.Amount,
                ProductId = rowMin.ProductId,
                RowDiscount = rowMin.Discount,
                ReceiptId = rowMin.ReceiptId
            };

            return (await RepoDbSet.AddAsync(receiptRow)).Entity;
        }
        
        public async Task<ReceiptRow> FindRowAndRelatedDataAsync(int id)
        {
            return await RepoDbSet
                .Include(row => row.Receipt)
                .Include(row => row.Product)
                .ThenInclude(product => product.Prices)
                .Include(row => row.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.Prices)
                .Include(row => row.RowParticpantLoanRows)
                .ThenInclude(row => row.Loan)
                .ThenInclude(loan => loan.LoanTaker)
                .FirstOrDefaultAsync(row => row.Id == id);
        }
    }
}