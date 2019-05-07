using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRepositoryAsync : BaseRepositoryAsync<Receipt>, IReceiptRepository
    {
        public ReceiptRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Receipt>> AllAsync()
        {
            return await RepoDbSet
                .Include(receipt => receipt.ReceiptManager)
                .ToListAsync();
        }

        public Task<Receipt> FindMinAsync(int receiptId)
        {
            return base.FindAsync(receiptId);
        }

        public override async Task<Receipt> FindAsync(params object[] id)
        {
            var receipt = await RepoDbSet.FindAsync(id);

            if (receipt != null)
            {
                await RepoDbContext.Entry(receipt).Reference(r => r.ReceiptManager).LoadAsync();
                await RepoDbContext.Entry(receipt).Collection(r => r.ReceiptParticipants).LoadAsync();
            }

            return receipt;
        }

        public async Task<List<DALReceiptDTO>> AllUserReceipts(int userId, bool isFinalized)
        {
            var receipts = await RepoDbSet
                .Include(receipt => receipt.ReceiptRows)
                    .ThenInclude(row => row.ReceiptRowChanges)
                        .ThenInclude(receiptRowChange => receiptRowChange.Change)
                            .ThenInclude(change => change.Prices)
                .Include(receipt => receipt.ReceiptRows)
                    .ThenInclude(row => row.Product)
                        .ThenInclude(product => product.Prices)
                .Where(receipt => receipt.ReceiptManagerId == userId && receipt.IsFinalized == isFinalized)
                .ToListAsync();

            return receipts
                .Select(ReceiptMapper.FromDomain)
                .ToList();
        }

        public async Task<Receipt> AddAsync(ReceiptDTO receiptDTO)
        {
            var receipt = new Receipt()
            {
                ReceiptManagerId = receiptDTO.ReceiptManagerId,
                CreatedTime = receiptDTO.CreatedTime,
                IsFinalized = receiptDTO.IsFinalized
            };
            return (await RepoDbSet.AddAsync(receipt)).Entity;
        }
    }
}