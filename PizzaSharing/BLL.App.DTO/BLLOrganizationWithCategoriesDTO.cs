using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLOrganizationWithCategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BLLCategoryMinDTO> Categories { get; set; }
    }
}