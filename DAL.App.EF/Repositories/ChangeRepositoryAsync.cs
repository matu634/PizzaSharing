using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ChangeRepositoryAsync : BaseRepositoryAsync<Change> , IChangeRepository
    {
        public ChangeRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }
}