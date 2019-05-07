using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
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

        /// <summary>
        ///  Returns Amount, Product(...), Changes(...), Participants(...), Discount, ReceiptId, RowId, Cost, 
        /// </summary>
        /// <param name="receiptId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<List<DALReceiptRowDTO>> AllReceiptsRows(int receiptId, DateTime time)
        {
            var rows = await RepoDbSet
                .Include(row => row.Product)
                .ThenInclude(product => product.ProductName)
                .ThenInclude(name => name.Translations)
                .Include(row => row.Product)
                .ThenInclude(product => product.ProductDescription)
                .ThenInclude(desc => desc.Translations)
                .Include(row => row.Product)
                .ThenInclude(product => product.Prices)
                .Include(row => row.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.ChangeName)
                .ThenInclude(name => name.Translations)
                .Include(row => row.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.Prices)
                .Include(row => row.RowParticipantLoanRows)
                .ThenInclude(row => row.Loan)
                .ThenInclude(loan => loan.LoanTaker)
                .Where(row => row.ReceiptId == receiptId)
                .ToListAsync();

            return rows
                .Select(row => ReceiptRowMapper.FromDomain(row, time))
                .Where(dto => dto != null)
                .ToList();
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
                .ThenInclude(product => product.ProductName)
                .ThenInclude(name => name.Translations)
                .Include(row => row.Product)
                .ThenInclude(product => product.ProductDescription)
                .ThenInclude(desc => desc.Translations)
                .Include(row => row.Product)
                .ThenInclude(product => product.Prices)
                .Include(row => row.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.ChangeName)
                .ThenInclude(name => name.Translations)
                .Include(row => row.ReceiptRowChanges)
                .ThenInclude(receiptRowChange => receiptRowChange.Change)
                .ThenInclude(change => change.Prices)
                .Include(row => row.RowParticipantLoanRows)
                .ThenInclude(row => row.Loan)
                .ThenInclude(loan => loan.LoanTaker)
                .FirstOrDefaultAsync(row => row.Id == id);
        }
    }
}