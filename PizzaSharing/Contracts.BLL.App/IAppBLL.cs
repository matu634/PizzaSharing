using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IPriceService Prices { get; }
        
        IAppService AppService { get; }
        
        IReceiptsService ReceiptsService { get; }
        
        ILoanService LoanService { get; }
    }
}