namespace BLL.App.DTO
{
    public class BLLAppUserDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        protected bool Equals(BLLAppUserDTO other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetType() == this.GetType() && Equals((BLLAppUserDTO) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}