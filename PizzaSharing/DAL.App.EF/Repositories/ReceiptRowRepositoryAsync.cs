using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.masirg.Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PublicApi.DTO;
using ReceiptMapper = PublicApi.DTO.Mappers.ReceiptMapper;

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

        /// <summary>
        /// Adds row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="userId"></param>
        /// <returns>receiptRowId</returns>
        public async Task<int?> AddAsync(DALReceiptRowDTO row)
        {
            
            var receiptRow = ReceiptRowMapper.FromDAL(row);
            if (receiptRow == null) return null;
            
            var receiptEntity = (await RepoDbSet.AddAsync(receiptRow)).Entity;
            
            EntityCreationCache.Add(receiptEntity.Id, receiptEntity);

            return receiptEntity.Id;
        }
        
        /// <summary>
        /// Returns Amount, Product(...), Changes(...), Participants(...), Discount, ReceiptId, RowId, Cost, 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DALReceiptRowDTO> FindRowAndRelatedDataAsync(int id)
        {
            var receiptRow = await RepoDbSet
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
            
            if (receiptRow == null) return null;
            
            return ReceiptRowMapper.FromDomain(receiptRow, DateTime.Now);
        }
        /// <summary>
        /// Updates Row amount, if 1)Row is found, 2)UserId matches ManagerId, 3)Receipt is not finalized
        /// </summary>
        /// <param name="receiptRowId"></param>
        /// <param name="newAmount"></param>
        /// <param name="userId"></param>
        /// <returns>ReceiptRowId</returns>
        public async Task<int?> UpdateRowAmount(int receiptRowId, int newAmount, int userId)
        {
            var receiptRow = await RepoDbSet
                .Include(row => row.Receipt)
                .FirstOrDefaultAsync(row => row.Id == receiptRowId);

            if (receiptRow == null) return null;
            if (receiptRow.Receipt.ReceiptManagerId != userId || receiptRow.Receipt.IsFinalized) return null;

            receiptRow.Amount = newAmount;
            return receiptRow.Id;
        }

        /// <summary>
        /// Removes row if 1)Row is found, 2)UserId matches ManagerId, 3)Receipt is not finalized
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> RemoveRowAsync(int rowId, int userId)
        {
            var receiptRow = await RepoDbSet
                .Include(row => row.Receipt)
                .FirstOrDefaultAsync(row => row.Id == rowId);

            if (receiptRow == null) return false;
            if (receiptRow.Receipt.ReceiptManagerId != userId || receiptRow.Receipt.IsFinalized) return false;
            RepoDbSet.Remove(receiptRow);
            return true;
        }
    }
}