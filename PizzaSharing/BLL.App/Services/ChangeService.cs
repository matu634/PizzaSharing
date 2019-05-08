using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class ChangeService : BaseService<IAppUnitOfWork>, IChangeService
    {
        public ChangeService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<bool> AddChangeAsync(BLLChangeDTO changeDto)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(changeDto.OrganizationId);
            if (organization?.Categories == null || organization.Categories.Count == 0) return false;

            var organizationCategoryIds = organization.Categories.Select(dto => dto.Id).ToList();

            foreach (var category in changeDto.Categories)
            {
                //Make sure select categories belong to selected Organization
                if (!organizationCategoryIds.Contains(category.Id)) return false;
            }

            //1.Add change
            var change = await Uow.Changes.AddAsync(ChangeMapper.FromBLL(changeDto));

            //2. Add change categories
            foreach (var category in changeDto.Categories)
            {
                await Uow.ChangesInCategories.AddAsync(change.Id, category.Id);
            }
            
            //3. Add price
            var priceDTO = new DALPriceDTO()
            {
                Value = changeDto.CurrentPrice,
                ChangeId = change.Id,
                ValidFrom = DateTime.MinValue,
                ValidTo = DateTime.MaxValue,
            };
            await Uow.Prices.AddAsync(priceDTO);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<BLLChangeDTO> GetChangeAsync(int changeId)
        {
            var changeDTO = await Uow.Changes.FindDTOAsync(changeId);
            if (changeDTO == null) return null;
            return ChangeMapper.FromDAL(changeDTO);
        }

        public async Task<bool> DeleteChangeAsync(int changeId)
        {
            var result = await Uow.Changes.RemoveSoft(changeId);
            if (result == false) return false;
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditChange(BLLChangeDTO changeDto)
        {
            var organization = await Uow.Organizations.FindWithCategoriesAsync(changeDto.OrganizationId);
            if (organization?.Categories == null || organization.Categories.Count == 0) return false;

            var organizationCategoryIds = organization.Categories.Select(dto => dto.Id).ToList();

            foreach (var category in changeDto.Categories)
            {
                if (!organizationCategoryIds.Contains(category.Id)) return false;
            }

            //1.Edit product entity
            var change = await Uow.Changes.EditAsync(ChangeMapper.FromBLL2(changeDto));
            if (change == null) return false;
            
            //2. Edit product categories
            await Uow.ChangesInCategories.RemoveByChangeId(change.Id);
            
            foreach (var category in changeDto.Categories)
            {
                await Uow.ChangesInCategories.AddAsync(change.Id, category.Id);
            }
            
            //3. Add price
            var priceDTO = new DALPriceDTO()
            {
                Value = changeDto.CurrentPrice,
                ChangeId = change.Id,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.MaxValue,
            };
            await Uow.Prices.EditAsync(priceDTO);
            await Uow.SaveChangesAsync();
            return true;
        }
    }
}