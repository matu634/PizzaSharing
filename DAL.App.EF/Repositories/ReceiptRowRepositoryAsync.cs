using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRowRepositoryAsync : BaseRepositoryAsync<ReceiptRow>, IReceiptRowRepository
    {
        public ReceiptRowRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }
}