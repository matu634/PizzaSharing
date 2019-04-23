using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain;
using PublicApi.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrganizationRepository : IBaseRepositoryAsync<Organization>
    {
        Task<List<OrganizationDTO>> AllDtoAsync(DateTime dateTime);
        
        Task<List<DALOrganizationMinDTO>> AllDtoMinAsync();
        
        Task<DALOrganizationMinDTO> FindMinDTOAsync(int id);

        Task<DALOrganizationDTO> FindWithCategoriesAsync(int id);
    }
}