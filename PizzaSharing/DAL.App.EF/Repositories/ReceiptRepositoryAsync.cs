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
    public class ReceiptRepositoryAsync : BaseRepositoryAsync<Receipt>, IReceiptRepository
    {
        public ReceiptRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
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

        public async Task<int?> AddAsync(DALReceiptDTO receiptDTO)
        {
            var receipt = ReceiptMapper.FromDAL(receiptDTO);
            var addedEntity = (await RepoDbSet.AddAsync(receipt)).Entity;
            if (addedEntity == null) return null;
            EntityCreationCache.Add(addedEntity.Id, addedEntity);
            return addedEntity.Id;
        }

        public async Task<bool> SetFinalized(int receiptId)
        {
            var receipt = await RepoDbSet.FindAsync(receiptId);
            if (receipt == null) return false;
            receipt.IsFinalized = true;
            receipt.CreatedTime = DateTime.Now;
            return true;
        }


        public async Task<DALReceiptDTO> FindReceiptAsync(int receiptId)
        {
            var receipt = await RepoDbSet.FindAsync(receiptId);
            if (receipt == null) return null;
            await RepoDbContext.Entry(receipt).Collection(r => r.ReceiptParticipants).LoadAsync();
            await RepoDbContext.Entry(receipt).Reference(r => r.ReceiptManager).LoadAsync();

            return ReceiptMapper.FromDomain2(receipt);
        }
    }
}