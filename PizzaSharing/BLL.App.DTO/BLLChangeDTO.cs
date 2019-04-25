using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class BLLChangeDTO
    {
        public BLLChangeDTO(int id, string name, decimal currentPrice, List<BLLCategoryMinDTO> categories)
        {
            Id = id;
            Name = name;
            CurrentPrice = currentPrice;
            Categories = categories;
        }

        public BLLChangeDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public int OrganizationId { get; set; }
        public List<BLLCategoryMinDTO> Categories { get; set; }
    }
}