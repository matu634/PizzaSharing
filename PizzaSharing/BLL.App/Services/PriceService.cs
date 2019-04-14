using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class PriceService : BaseEntityService<Price, IAppUnitOfWork>, IPriceService
    {
        public PriceService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public override async Task<IEnumerable<Price>> AllAsync()
        {
            return await Uow.Prices.AllAsync();
        }

        public override async Task<Price> FindAsync(params object[] id)
        {
            return await Uow.Prices.FindAsync(id);
        }
    }
}