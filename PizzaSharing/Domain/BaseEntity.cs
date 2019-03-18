using Contracts.DAL.Base;

namespace Domain
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}