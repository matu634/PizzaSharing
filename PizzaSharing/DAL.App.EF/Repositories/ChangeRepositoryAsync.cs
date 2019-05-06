using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
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

        public async Task<List<DALChangeDTO>> AllAsync(int organizationId)
        {
            var changes = await RepoDbSet
                .Include(change => change.Prices)
                .Include(change => change.ChangeInCategories)
                .ThenInclude(obj => obj.Category)
                .Include(change => change.ChangeName)
                .ThenInclude(name => name.Translations)
                .Where(change => change.IsDeleted == false && change.OrganizationId == organizationId)
                .ToListAsync();

            return changes
                .Select(ChangeMapper.FromDomain)
                .Where(dto => dto.CurrentPrice != -1.0m)
                .ToList();
        }

        public async Task<DALChangeDTO> AddAsync(DALChangeDTO changeDTO)
        {
            var change = ChangeMapper.FromDAL(changeDTO);

            change = (await RepoDbSet.AddAsync(change)).Entity;
            if (change == null) return null;
            await RepoDbContext.Entry(change).Reference(c => c.ChangeName).LoadAsync();
            await RepoDbContext.Entry(change.ChangeName).Collection(c => c.Translations).LoadAsync();

            return ChangeMapper.FromDomain(change);
        }

        public async Task<DALChangeDTO> FindDTOAsync(int changeId)
        {
            var change = await RepoDbSet
                .Include(c => c.ChangeName)
                .ThenInclude(name => name.Translations)
                .Include(c => c.ChangeInCategories)
                .ThenInclude(obj => obj.Category)
                .Include(c => c.Prices)
                .Where(c => c.IsDeleted == false && c.Id == changeId)
                .SingleOrDefaultAsync();
            
            if (change == null) return null;

            var currentPrice = PriceFinder.ForChange(change, change.Prices, DateTime.Now);
            if (currentPrice == null) return null;

            return ChangeMapper.FromDomain(change);
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
            
            await RepoDbContext.Entry(change).Reference(c => c.ChangeName).LoadAsync();
            await RepoDbContext.Entry(change.ChangeName).Collection(c => c.Translations).LoadAsync();

                        
            change.ChangeName.SetTranslation(changeDTO.Name);

            return ChangeMapper.FromDomain(change);
        }
    }
}