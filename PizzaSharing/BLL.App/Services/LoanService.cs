using System.Threading.Tasks;
using ee.itcollege.masirg.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Enums;

namespace BLL.App.Services
{
    public class LoanService : BaseService<IAppUnitOfWork>, ILoanService
    {
        public LoanService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<int> ChangeLoanStatusAsync(int loanId, LoanStatus newStatus, int userId)
        {
            var loan = await Uow.Loans.FindAsync(loanId);
            if (loan == null) return -1;
            if (loan.LoanGiverId != userId && loan.LoanTakerId != userId) return -1;

            if (loan.Status == LoanStatus.Paid) return -1; // Paid loans can't be changed
            if (loan.Status == newStatus) return (int) newStatus;

            if (loan.LoanGiverId == userId)
            {
                if (loan.Status == LoanStatus.AwaitingConfirmation && newStatus == LoanStatus.Paid ||
                    loan.Status == LoanStatus.AwaitingConfirmation && newStatus == LoanStatus.Rejected ||
                    loan.Status == LoanStatus.NotPaid && newStatus == LoanStatus.Paid ||
                    loan.Status == LoanStatus.Rejected && newStatus == LoanStatus.Paid)
                {
                    await Uow.Loans.ChangeStatusAsync(loanId, newStatus);
                    await Uow.SaveChangesAsync();
                    return (int) newStatus;
                }

                return -1;
            }

            if ((loan.Status == LoanStatus.NotPaid || loan.Status == LoanStatus.Rejected) &&
                newStatus == LoanStatus.AwaitingConfirmation)
            {
                await Uow.Loans.ChangeStatusAsync(loanId, newStatus);
                await Uow.SaveChangesAsync();
                return (int) newStatus;
            }

            return -1;

        }
    }
}