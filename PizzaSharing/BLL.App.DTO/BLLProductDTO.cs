using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLProductDTO
    {
        public BLLProductDTO(int id, string productName, decimal currentPrice, List<string> categories)
        {
            Id = id;
            ProductName = productName;
            CurrentPrice = currentPrice;
            Categories = categories;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<string> Categories { get; set; }
    }
}