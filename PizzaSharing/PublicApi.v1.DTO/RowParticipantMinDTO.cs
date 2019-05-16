using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class RowParticipantMinDTO
    {
        [Required]
        public int? UserId { get; set; }
        
        [Required]
        public int? RowId { get; set; }
        
        [Required]
        public decimal? Involvement { get; set; }
    }
}