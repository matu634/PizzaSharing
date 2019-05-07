using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DALProductDTO> Products { get; set; }
        public List<DALChangeDTO> Changes { get; set; }
        public int OrganizationId { get; set; }
    }
}