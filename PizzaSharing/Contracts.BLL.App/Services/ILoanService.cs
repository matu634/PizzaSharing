using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface ILoanService : IBaseService
    {
        void MarkLoanPaid();

        void SubmitPaidRequest();

        void DeclinePaidRequest();
    }
}