using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<string> Categories { get; set; }
    }
}