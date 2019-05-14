using System;
using Contracts.BLL.App.Services;
using ee.itcollege.masirg.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IAppService AppService { get; }
        
        IReceiptsService ReceiptsService { get; }
        
        ILoanService LoanService { get; }

        IAppUserService AppUserService { get; }

        IOrganizationsService OrganizationsService { get; }
        
        IProductService ProductService { get; }
        
        IChangeService ChangeService { get; }

        ICategoryService CategoryService { get; }
    }
}