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
        Task<List<DALOrganizationDTO>> AllWithCategoriesAndProducts(DateTime dateTime);
        
        Task<List<DALOrganizationDTO>> AllMinDTOAsync();
        
        Task<DALOrganizationDTO> FindMinDTOAsync(int id);

        Task<DALOrganizationDTO> FindWithCategoriesAsync(int id);
        
        Task AddAsync(DALOrganizationDTO organizationDTO);
    }
}