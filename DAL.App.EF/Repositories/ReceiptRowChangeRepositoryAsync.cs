using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRowChangeRepositoryAsync : BaseRepositoryAsync<ReceiptRowChange>, IReceiptRowChangeRepository
    {
        public ReceiptRowChangeRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }
}