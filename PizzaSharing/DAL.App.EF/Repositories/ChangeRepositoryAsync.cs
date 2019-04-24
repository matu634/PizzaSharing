using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ChangeRepositoryAsync : BaseRepositoryAsync<Change>, IChangeRepository
    {
        public ChangeRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Change>> AllAsync()
        {
            return await RepoDbSet
                .Include(change => change.Organization)
                .ToListAsync();
        }


        public async Task<Change> FindAsync(int id)
        {
            var change = await RepoDbSet.FindAsync(id);
            if (change != null)
            {
                await RepoDbContext.Entry(change).Reference(c => c.Organization).LoadAsync();
            }

            return change;
        }

        public async Task<List<DALChangeDTO>> AllAsync(int organizationId)
        {
            var changes = await RepoDbSet
                .Include(change => change.Prices)
                .Include(change => change.ChangeInCategories)
                .ThenInclude(obj => obj.Category)
                .Where(change => change.IsDeleted == false && change.OrganizationId == organizationId)
                .ToListAsync();

            return changes
                .Select(change => new DALChangeDTO()
                {
                    Id = change.Id,
                    Name = change.ChangeName,
                    CurrentPrice = PriceFinder.ForChange(change, change.Prices, DateTime.Now) ?? -1.0m,
                    Categories = change.ChangeInCategories
                        .Where(obj => obj.Category.IsDeleted == false)
                        .Select(obj => new DALCategoryMinDTO(obj.CategoryId, obj.Category.CategoryName))
                        .ToList()
                })
                .Where(dto => dto.CurrentPrice != -1.0m)
                .ToList();
        }

        public async Task<DALChangeDTO> AddAsync(DALChangeDTO changeDTO)
        {
            var change = new Change()
            {
                OrganizationId = changeDTO.OrganizationId,
                ChangeName = changeDTO.Name
            };

            change = (await RepoDbSet.AddAsync(change)).Entity;

            return new DALChangeDTO()
            {
                Id = change.Id,
                Name = change.ChangeName,
                OrganizationId = change.OrganizationId
            };
        }

        public async Task<DALChangeDTO> FindDTOAsync(int changeId)
        {
            var change = await RepoDbSet
                .Include(c => c.ChangeInCategories)
                .ThenInclude(obj => obj.Category)
                .Include(c => c.Prices)
                .Where(c => c.IsDeleted == false && c.Id == changeId)
                .SingleOrDefaultAsync();
            if (change == null) return null;

            var currentPrice = PriceFinder.ForChange(change, change.Prices, DateTime.Now);
            if (currentPrice == null) return null;

            return new DALChangeDTO()
            {
                Id = change.Id,
                Name = change.ChangeName,
                CurrentPrice = currentPrice.Value,
                OrganizationId = change.OrganizationId,
                Categories = change.ChangeInCategories
                    .Select(obj => new DALCategoryMinDTO(obj.CategoryId, obj.Category.CategoryName))
                    .ToList()
            };
        }

        public async Task<bool> RemoveSoft(int changeId)
        {
            var change = await RepoDbSet.FindAsync(changeId);
            if (change == null) return false;
            change.IsDeleted = true;
            return true;
        }

        public async Task<DALChangeDTO> EditAsync(DALChangeDTO changeDTO)
        {
            var change = await RepoDbSet.FindAsync(changeDTO.Id);
            if (change == null) return null;
            
            change.ChangeName = changeDTO.Name;
            
            return new DALChangeDTO()
            {
                Id = change.Id,
                Name = change.ChangeName,
                OrganizationId = change.OrganizationId
            };
        }
    }
}