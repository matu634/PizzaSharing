using System;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow, IBaseServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
        }

        public IPriceService Prices => ServiceProvider.GetService<IPriceService>();
        
        public IAppService AppService => ServiceProvider.GetService<IAppService>();
        public IReceiptsService ReceiptsService => ServiceProvider.GetService<IReceiptsService>();
        public ILoanService LoanService => ServiceProvider.GetService<ILoanService>();
        public IAppUserService AppUserService => ServiceProvider.GetService<IAppUserService>();
        public IOrganizationsService OrganizationsService => ServiceProvider.GetService<IOrganizationsService>();
    }
}