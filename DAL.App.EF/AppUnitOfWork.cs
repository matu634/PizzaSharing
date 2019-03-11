using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        //Repo cache
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();
        private AppDbContext _appDbContext;

        public AppUnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        //----------------------------Repositories----------------------------
        
        
        public ICategoryRepository Categories =>
            GetOrCreateRepository(context => new CategoryRepositoryAsync(_appDbContext));

        public IChangeRepository Changes => 
            GetOrCreateRepository(context => new ChangeRepositoryAsync(_appDbContext));
        
        public ILoanRepository Loans => 
            GetOrCreateRepository(context => new LoanRepositoryAsync(_appDbContext));
        
        public ILoanRowRepository LoanRows => 
            GetOrCreateRepository(context => new LoanRowRepositoryAsync(_appDbContext));

        public IOrganizationRepository Organizations =>
            GetOrCreateRepository(context => new OrganizationRepositoryAsync(_appDbContext));

        public IPriceRepository Prices => 
            GetOrCreateRepository(context => new PriceRepositoryAsync(_appDbContext));

        public IProductInCategoryRepository ProductsInCategories =>
            GetOrCreateRepository(context => new ProductInCategoryRepositoryAsync(_appDbContext));

        public IProductRepository Products => 
            GetOrCreateRepository(context => new ProductRepositoryAsync(_appDbContext));
        
        public IReceiptRepository Receipts => 
            GetOrCreateRepository(context => new ReceiptRepositoryAsync(_appDbContext));

        public IReceiptParticipantRepository ReceiptParticipants =>
            GetOrCreateRepository(context => new ReceiptParticipantRepositoryAsync(_appDbContext));

        public IReceiptRowRepository ReceiptRows =>
            GetOrCreateRepository(context => new ReceiptRowRepositoryAsync(_appDbContext));

        public IReceiptRowChangeRepository ReceiptRowChanges =>
            GetOrCreateRepository(context => new ReceiptRowChangeRepositoryAsync(context));

        
        //----------------------------Get base repo Method----------------------------
        
        
        public IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return GetOrCreateRepository(context => new BaseRepositoryAsync<TEntity>(_appDbContext));
        }

        
        //----------------------------Caching repos method----------------------------
        
        
        private TRepository GetOrCreateRepository<TRepository>(Func<AppDbContext, TRepository> factoryMethod)
        {
            _repositoryCache.TryGetValue(typeof(TRepository), out var repoObject);
            if (repoObject != null)
            {
                return (TRepository) repoObject;
            }

            repoObject = factoryMethod(_appDbContext);
            _repositoryCache[typeof(TRepository)] = repoObject;
            return (TRepository) repoObject;
        }
        
        
        //----------------------------UoW methods----------------------------

        
        public virtual int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}