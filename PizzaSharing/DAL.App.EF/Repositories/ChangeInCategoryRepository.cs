using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ChangeInCategoryRepository : BaseRepositoryAsync<ChangeInCategory>, IChangeInCategoryRepository
    {
        public ChangeInCategoryRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task AddAsync(int changeId, int categoryId)
        {
            var obj = new ChangeInCategory()
            {
                ChangeId = changeId,
                CategoryId = categoryId
            };

            await RepoDbSet.AddAsync(obj);
        }

        public async Task RemoveByChangeId(int changeId)
        {
            var objects = await RepoDbSet
                .Where(obj => obj.ChangeId == changeId)
                .ToListAsync();

            RepoDbSet.RemoveRange(objects);
        }

        public async Task<List<DALChangeDTO>> GetChangesByCategoryIdsAsync(int[] categoryIds)
        {
            var changeInCategories = await RepoDbSet
                .Include(obj => obj.Category)
                .Include(obj => obj.Change)
                .ThenInclude(change => change.ChangeName)
                .ThenInclude(name => name.Translations)
                .Include(obj => obj.Change)
                .ThenInclude(change => change.Prices)
                .Where(obj => categoryIds.Contains(obj.CategoryId) && obj.Change.IsDeleted == false )
                .ToListAsync();

            return changeInCategories
                .Select(changeInCategory =>
                {
                    var change = changeInCategory.Change;
                    var price = PriceFinder.ForChange(change, change.Prices, DateTime.Now);
                    if (price == null) return null;
                    
                    return new DALChangeDTO()
                    {
                        Id = change.Id,
                        Name = change.ChangeName.Translate(),
                        CurrentPrice = price.Value,
                        OrganizationId = change.OrganizationId,
                        Categories = change.ChangeInCategories
                            .Select(obj => new DALCategoryMinDTO(obj.CategoryId, obj.Category.CategoryName))
                            .ToList()
                    };
                })
                .Where(dto => dto != null)
                .Distinct()
                .ToList();
        }
    }
}