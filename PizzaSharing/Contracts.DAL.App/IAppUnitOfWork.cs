using Contracts.DAL.App.Repositories;
using Contracts.DAL.App.Repositories.Identity;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IChangeRepository Changes { get; }
        ILoanRepository Loans { get; }
        ILoanRowRepository LoanRows { get; }
        IOrganizationRepository Organizations { get; }
        IPriceRepository Prices { get; }
        IProductInCategoryRepository ProductsInCategories { get; }
        IProductRepository Products { get; }
        IReceiptRepository Receipts { get; }
        IReceiptParticipantRepository ReceiptParticipants { get; }
        IReceiptRowRepository ReceiptRows { get; }
        IReceiptRowChangeRepository ReceiptRowChanges { get; }
        IAppUserRepository AppUsers { get; }
    }
}