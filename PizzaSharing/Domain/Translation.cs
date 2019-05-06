using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Translation : BaseEntity
    {
        public Translation(string culture, string value)
        {
            Culture = culture;
            Value = value;
        }

        public int MultiLangStringId { get; set; }
        public MultiLangString MultiLangString { get; set; }

        [MaxLength(12)]
        [Required]
        public string Culture { get; set; }

        [MaxLength(10240)]
        [Required]
        public string Value { get; set; }
    }
}