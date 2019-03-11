using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {   
        private readonly AppDbContext _appDbContext;

        private readonly IRepositoryProvider _repositoryProvider;

        public AppUnitOfWork(AppDbContext appDbContext, IRepositoryProvider repositoryProvider)
        {
            _appDbContext = appDbContext;
            _repositoryProvider = repositoryProvider;
        }

        
        //-------------------------------------------------Repositories-------------------------------------------------
        
        
        public ICategoryRepository Categories => _repositoryProvider.GetRepository<ICategoryRepository>();

        public IChangeRepository Changes => _repositoryProvider.GetRepository<IChangeRepository>();
        
        public ILoanRepository Loans => _repositoryProvider.GetRepository<ILoanRepository>();
        
        public ILoanRowRepository LoanRows => _repositoryProvider.GetRepository<ILoanRowRepository>();

        public IOrganizationRepository Organizations =>_repositoryProvider.GetRepository<IOrganizationRepository>();

        public IPriceRepository Prices => _repositoryProvider.GetRepository<IPriceRepository>();

        public IProductInCategoryRepository ProductsInCategories =>_repositoryProvider.GetRepository<IProductInCategoryRepository>();

        public IProductRepository Products => _repositoryProvider.GetRepository<IProductRepository>();
        
        public IReceiptRepository Receipts => _repositoryProvider.GetRepository<IReceiptRepository>();

        public IReceiptParticipantRepository ReceiptParticipants =>_repositoryProvider.GetRepository<IReceiptParticipantRepository>();

        public IReceiptRowRepository ReceiptRows =>_repositoryProvider.GetRepository<IReceiptRowRepository>();

        public IReceiptRowChangeRepository ReceiptRowChanges =>_repositoryProvider.GetRepository<IReceiptRowChangeRepository>();

        
        //---------------------------------------------Get base repo Method---------------------------------------------
        
        
        public IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return _repositoryProvider.GetRepositoryForEntity<TEntity>();
        }

        
        
        
        
        //-------------------------------------------------UoW methods--------------------------------------------------

        
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