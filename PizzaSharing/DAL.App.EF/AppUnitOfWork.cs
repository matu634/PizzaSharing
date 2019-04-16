using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.App.Repositories.Identity;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork, IAppUnitOfWork
    {   

        public AppUnitOfWork(IDataContext appDbContext, IBaseRepositoryProvider repositoryProvider) : base(appDbContext, repositoryProvider)
        {
        }

        
        //-------------------------------------------------Repositories-------------------------------------------------
        
        
        public ICategoryRepository Categories => RepositoryProvider.GetRepository<ICategoryRepository>();

        public IChangeRepository Changes => RepositoryProvider.GetRepository<IChangeRepository>();
        
        public ILoanRepository Loans => RepositoryProvider.GetRepository<ILoanRepository>();
        
        public ILoanRowRepository LoanRows => RepositoryProvider.GetRepository<ILoanRowRepository>();

        public IOrganizationRepository Organizations =>RepositoryProvider.GetRepository<IOrganizationRepository>();

        public IPriceRepository Prices => RepositoryProvider.GetRepository<IPriceRepository>();

        public IProductInCategoryRepository ProductsInCategories =>RepositoryProvider.GetRepository<IProductInCategoryRepository>();

        public IProductRepository Products => RepositoryProvider.GetRepository<IProductRepository>();
        
        public IReceiptRepository Receipts => RepositoryProvider.GetRepository<IReceiptRepository>();

        public IReceiptParticipantRepository ReceiptParticipants =>RepositoryProvider.GetRepository<IReceiptParticipantRepository>();

        public IReceiptRowRepository ReceiptRows =>RepositoryProvider.GetRepository<IReceiptRowRepository>();

        public IReceiptRowChangeRepository ReceiptRowChanges =>RepositoryProvider.GetRepository<IReceiptRowChangeRepository>();
        
        public IAppUserRepository AppUsers => RepositoryProvider.GetRepository<IAppUserRepository>();
    }
}