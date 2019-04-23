using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLProductDTO
    {
        public BLLProductDTO(int id, string productName, decimal currentPrice, List<BLLCategoryMinDTO> categories)
        {
            Id = id;
            ProductName = productName;
            CurrentPrice = currentPrice;
            Categories = categories;
        }

        public BLLProductDTO(){
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ProductName { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<BLLCategoryMinDTO> Categories { get; set; }
    }
}