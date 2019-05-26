using System.ComponentModel.DataAnnotations;
using Enums;

namespace PublicApi.DTO
{
    public class LoanIdAndStatus
    {
        [Required]
        public int? LoanId { get; set; }
        
        [Required]
        [EnumDataType(typeof(LoanStatus), ErrorMessage = "LoanStatus value doesn't exist within enum")]
        public LoanStatus? Status { get; set; }
    }
}