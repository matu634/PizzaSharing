using System;
using Contracts.BLL.Base.Services;

namespace BLL.Base.Services
{
    public class BaseService<TUnitOfWork> : IBaseService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
        
        protected TUnitOfWork Uow;

        public BaseService(TUnitOfWork uow)
        {
            Uow = uow;
        }
    }
}