using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALOrganizationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DALCategoryDTO> Categories { get; set; }
    }
}