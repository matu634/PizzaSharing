using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLCategoryDTO
    {
        public int OrganizationId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<BLLProductDTO> Products { get; set; }
        public List<BLLChangeDTO> Changes { get; set; }
    }
}