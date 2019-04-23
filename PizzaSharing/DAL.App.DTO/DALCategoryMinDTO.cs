namespace DAL.App.DTO
{
    public class DALCategoryMinDTO
    {
        public DALCategoryMinDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}