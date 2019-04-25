using Contracts.DAL.Base;

namespace Domain
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        protected bool Equals(BaseEntity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            var item = obj as BaseEntity;

            if (item == null) return false;
            return Id == item.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}