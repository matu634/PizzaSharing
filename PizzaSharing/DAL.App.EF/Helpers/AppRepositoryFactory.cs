using System;
using System.Collections.Generic;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.App.Repositories.Identity;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.App.EF.Repositories.Identity;
using DAL.Base.EF.Helpers;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory
    {
        public AppRepositoryFactory()
        {
            Add<IOrganizationRepository>(context => new OrganizationRepositoryAsync(context));
            
            Add<ICategoryRepository>(context => new CategoryRepositoryAsync(context));
            
            Add<IProductRepository>(context => new ProductRepositoryAsync(context));
            
            Add<IProductInCategoryRepository>(context => new ProductInCategoryRepositoryAsync(context));

            Add<IChangeRepository>(context => new ChangeRepositoryAsync(context));

            Add<ILoanRepository>(context => new LoanRepositoryAsync(context));
            
            Add<ILoanRowRepository>(context => new LoanRowRepositoryAsync(context));

            Add<IPriceRepository>(context => new PriceRepositoryAsync(context));

            Add<IReceiptRepository>(context => new ReceiptRepositoryAsync(context));
            
            Add<IReceiptRowRepository>(context => new ReceiptRowRepositoryAsync(context));
            
            Add<IReceiptRowChangeRepository>(context => new ReceiptRowChangeRepositoryAsync(context));
            
            Add<IReceiptParticipantRepository>(context => new ReceiptParticipantRepositoryAsync(context));
            
            Add<IAppUserRepository>(context => new AppUserRepository(context));
            
            Add<IChangeInCategoryRepository>(context => new ChangeInCategoryRepository(context));
        }
    }
}