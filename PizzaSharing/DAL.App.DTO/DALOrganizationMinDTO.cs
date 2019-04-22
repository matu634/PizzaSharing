namespace DAL.App.DTO
{
    public class DALOrganizationMinDTO
    {
        public DALOrganizationMinDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}