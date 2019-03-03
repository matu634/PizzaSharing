using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRowChangeRepository : BaseRepository<ReceiptRowChange>, IReceiptRowChangeRepository
    {
        public ReceiptRowChangeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}