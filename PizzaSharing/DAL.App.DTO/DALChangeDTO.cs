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
    }
}