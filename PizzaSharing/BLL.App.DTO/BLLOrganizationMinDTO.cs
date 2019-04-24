namespace BLL.App.DTO
{
    public class BLLOrganizationMinDTO
    {
        public BLLOrganizationMinDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public BLLOrganizationMinDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}