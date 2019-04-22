using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLCategoryDTO
    {
        public BLLCategoryDTO(int categoryId, string categoryName, List<string> productNames, List<string> changeNames)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            ProductNames = productNames;
            ChangeNames = changeNames;
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductNames { get; set; }
        public List<string> ChangeNames { get; set; }
    }
}