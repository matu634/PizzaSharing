using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLOrganizationAllDTO
    {
        public BLLOrganizationDTO Organization { get; set; }
        public List<BLLCategoryDTO> Categories { get; set; }
        public List<BLLProductDTO> Products { get; set; }
        public List<BLLChangeDTO> Changes { get; set; }
    }
}