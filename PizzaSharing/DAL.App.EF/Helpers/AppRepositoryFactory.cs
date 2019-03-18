using System;
using System.Collections.Generic;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Helpers;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory
    {
        public AppRepositoryFactory()
        {
            RepoFactoryMethods.Add(typeof(IOrganizationRepository), context => new OrganizationRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(ICategoryRepository), context => new CategoryRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(IProductRepository), context => new ProductRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(IProductInCategoryRepository), context => new ProductInCategoryRepositoryAsync(context));

            RepoFactoryMethods.Add(typeof(IChangeRepository), context => new ChangeRepositoryAsync(context));

            RepoFactoryMethods.Add(typeof(ILoanRepository), context => new LoanRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(ILoanRowRepository), context => new LoanRowRepositoryAsync(context));

            RepoFactoryMethods.Add(typeof(IPriceRepository), context => new PriceRepositoryAsync(context));

            RepoFactoryMethods.Add(typeof(IReceiptRepository), context => new ReceiptRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(IReceiptRowRepository), context => new ReceiptRowRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(IReceiptRowChangeRepository), context => new ReceiptRowChangeRepositoryAsync(context));
            
            RepoFactoryMethods.Add(typeof(IReceiptParticipantRepository), context => new ReceiptParticipantRepositoryAsync(context));
        }
    }
}