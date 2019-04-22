using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLChangeDTO
    {
        public BLLChangeDTO(int id, string name, decimal currentPrice, List<string> categoryNames)
        {
            Id = id;
            Name = name;
            CurrentPrice = currentPrice;
            CategoryNames = categoryNames;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<string> CategoryNames { get; set; }
    }
}