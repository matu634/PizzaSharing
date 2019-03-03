using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ChangeRepository : BaseRepository<Change> , IChangeRepository
    {
        public ChangeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}