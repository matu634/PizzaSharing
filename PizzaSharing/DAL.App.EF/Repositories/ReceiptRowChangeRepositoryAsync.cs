using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.masirg.Contracts.DAL.Base;
using ee.itcollege.masirg.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRowChangeRepositoryAsync : BaseRepositoryAsync<ReceiptRowChange>, IReceiptRowChangeRepository
    {
        public ReceiptRowChangeRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<ReceiptRowChange>> AllAsync()
        {
            return await RepoDbSet
                .Include(rowChange => rowChange.ReceiptRow)
                .Include(rowChange => rowChange.Change)
                .ToListAsync();
        }

        public override async Task<ReceiptRowChange> FindAsync(params object[] id)
        {
            var rowChange = await RepoDbSet.FindAsync(id);

            if (rowChange != null)
            {
                await RepoDbContext.Entry(rowChange).Reference(rowC => rowC.Change).LoadAsync();
                await RepoDbContext.Entry(rowChange).Reference(rowC => rowC.ReceiptRow).LoadAsync();
            }

            return rowChange;
        }

        public async Task AddAsync(int changeId, int receiptRowId)
        {
            var receiptRowChange = new ReceiptRowChange()
            {
                ChangeId = changeId,
                ReceiptRowId = receiptRowId
            };
            await RepoDbSet.AddAsync(receiptRowChange);
        }

        public async Task<bool> RemoveWhereAsync(int changeId, int rowId)
        {
            var rowChange = await RepoDbSet
                .FirstOrDefaultAsync(change => change.ChangeId == changeId && change.ReceiptRowId == rowId);
            if (rowChange == null) return false;
            RepoDbSet.Remove(rowChange);
            return true;
        }
    }
}