using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.masirg.BLL.Base;
using ee.itcollege.masirg.Contracts.BLL.Base.Helpers;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow, IBaseServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
        }
        public IAppService AppService => ServiceProvider.GetService<IAppService>();
        public IReceiptsService ReceiptsService => ServiceProvider.GetService<IReceiptsService>();
        public ILoanService LoanService => ServiceProvider.GetService<ILoanService>();
        public IAppUserService AppUserService => ServiceProvider.GetService<IAppUserService>();
        public IOrganizationsService OrganizationsService => ServiceProvider.GetService<IOrganizationsService>();
        public IProductService ProductService => ServiceProvider.GetService<IProductService>();
        public IChangeService ChangeService => ServiceProvider.GetService<IChangeService>();
        public ICategoryService CategoryService => ServiceProvider.GetService<ICategoryService>();
    }
}