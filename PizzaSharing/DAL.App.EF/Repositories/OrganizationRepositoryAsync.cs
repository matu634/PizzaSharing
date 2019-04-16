using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrganizationRepositoryAsync : BaseRepositoryAsync<Organization>, IOrganizationRepository
    {
        public OrganizationRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<OrganizationDTO>> AllDtoAsync()
        {
            return await RepoDbSet
                .Select(organization => new OrganizationDTO()
                    {Id = organization.Id, Name = organization.OrganizationName})
                .ToListAsync();
        }
    }
}