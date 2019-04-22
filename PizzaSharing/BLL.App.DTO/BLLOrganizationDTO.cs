namespace BLL.App.DTO
{
    public class BLLOrganizationDTO
    {
        public BLLOrganizationDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}