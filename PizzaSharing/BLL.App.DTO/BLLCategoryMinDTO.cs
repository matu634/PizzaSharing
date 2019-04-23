namespace BLL.App.DTO
{
    public class BLLCategoryMinDTO
    {
        public BLLCategoryMinDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public BLLCategoryMinDTO(int id)
        {
            Id = id;
        }

        public BLLCategoryMinDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}