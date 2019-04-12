using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IPriceService Prices { get; }
        
        //TODO: public facing services
        //IPizzaOrderService PizzaOrderService
    }
}