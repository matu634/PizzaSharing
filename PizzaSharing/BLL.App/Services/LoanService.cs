using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class LoanService : BaseService<IAppUnitOfWork>, ILoanService
    {
        public LoanService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public void MarkLoanPaid()
        {
            throw new System.NotImplementedException();
        }

        public void SubmitPaidRequest()
        {
            throw new System.NotImplementedException();
        }

        public void DeclinePaidRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}