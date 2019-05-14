using System;
using ee.itcollege.masirg.Contracts.BLL.Base.Services;

namespace ee.itcollege.masirg.BLL.Base.Services
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