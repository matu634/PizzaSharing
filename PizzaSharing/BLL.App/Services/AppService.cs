using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using PublicApi.DTO;

namespace BLL.App.Services
{
    public class AppService : BaseService<IAppUnitOfWork>, IAppService
    {
        public AppService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<BLLUserDashboardDTO> GetUserDashboard(int userId)
        {
            return new BLLUserDashboardDTO
            {
                Loans = (await Uow.Loans.AllUserGivenLoans(userId))
                    .Select(LoanGivenMapper.FromDAL)
                    .ToList(),
                Debts = (await Uow.Loans.AllUserTakenLoans(userId))
                    .Select(LoanTakenMapper.FromDAL)
                    .ToList(),
                OpenReceipts = (await Uow.Receipts.AllUserReceipts(userId, false))
                    .Select(ReceiptMapper.FromDAL)
                    .ToList(),
                ClosedReceipts = (await Uow.Receipts.AllUserReceipts(userId, true))
                    .Select(ReceiptMapper.FromDAL)
                    .ToList()
            };
        }

        public async Task<List<BLLOrganizationDTO>> GetOrganizationsCategoriesAndProductsAsync(int receiptId)
        {
            var receipt = await Uow.Receipts.FindAsync(receiptId);
            if (receipt == null) return null;
            var time = receipt.IsFinalized == false ? DateTime.Now : receipt.CreatedTime;
            var organizations = await Uow.Organizations.AllWithCategoriesAndProducts(time);
            if (organizations == null) return null;

            return organizations.Select(OrganizationMapper.FromDAL).ToList();
        }

        public List<ChangeDTO> GetValidProductsChanges()
        {
            throw new System.NotImplementedException();
        }

        public List<AppUserDTO> GetValidParticipants()
        {
            throw new System.NotImplementedException();
        }
    }
}