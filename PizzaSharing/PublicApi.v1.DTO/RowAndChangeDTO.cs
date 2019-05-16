using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class RowAndChangeDTO
    {
        [Required]
        public int? ComponentId { get; set; }
        
        [Required]
        public int? RowId { get; set; }
    }
}