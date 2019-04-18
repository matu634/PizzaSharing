using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class OrganizationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}