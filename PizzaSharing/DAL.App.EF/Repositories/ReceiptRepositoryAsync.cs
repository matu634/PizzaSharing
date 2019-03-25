using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

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

        public override async Task<Receipt> FindAsync(params object[] id)
        {
            var receipt = await RepoDbSet.FindAsync(id);

            if (receipt != null)
            {
                await RepoDbContext.Entry(receipt).Reference(r => r.ReceiptManager).LoadAsync();
            }

            return receipt;
        }
    }
}