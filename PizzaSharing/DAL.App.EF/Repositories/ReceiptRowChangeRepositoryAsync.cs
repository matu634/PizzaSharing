using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

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
    }
}