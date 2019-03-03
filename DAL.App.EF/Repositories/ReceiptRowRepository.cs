using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReceiptRowRepository : BaseRepository<ReceiptRow>, IReceiptRowRepository
    {
        public ReceiptRowRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}