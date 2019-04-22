using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class DALCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ProductNames { get; set; }
        public List<string> ChangeNames { get; set; }
    }
}