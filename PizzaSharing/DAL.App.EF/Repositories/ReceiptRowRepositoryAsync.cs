using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

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
    }
}