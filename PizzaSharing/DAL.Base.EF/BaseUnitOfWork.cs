using System;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected readonly DbContext UOWDbContext;
        protected readonly IBaseRepositoryProvider RepositoryProvider;

        public BaseUnitOfWork(IDataContext dataContext, IBaseRepositoryProvider repositoryProvider)
        {
            UOWDbContext = dataContext as DbContext ?? throw new ArgumentException("dataContext has to inherit DbContext (from Entity Framework)");
            RepositoryProvider = repositoryProvider;
        }

        public IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return RepositoryProvider.GetRepositoryForEntity<TEntity>();
        }

        public int SaveChanges()
        {
            return UOWDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();
        }
    }
}
