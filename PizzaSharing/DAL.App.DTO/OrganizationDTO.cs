using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class OrganizationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}