using Contracts.DAL.Base;

namespace Domain
{
    public class ChangeInCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ChangeId { get; set; }
        public Change Change { get; set; }
    }
}