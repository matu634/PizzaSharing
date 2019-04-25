using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALChangeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<DALCategoryMinDTO> Categories { get; set; }
        public int OrganizationId { get; set; }

        protected bool Equals(DALChangeDTO other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            var item = obj as DALChangeDTO;
            if (item == null) return false;
            
            
            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}