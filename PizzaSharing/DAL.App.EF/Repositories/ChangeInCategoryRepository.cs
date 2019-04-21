using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class ChangeInCategoryRepository : BaseRepositoryAsync<ChangeInCategory>, IChangeInCategoryRepository
    {
        public ChangeInCategoryRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }
    }
}