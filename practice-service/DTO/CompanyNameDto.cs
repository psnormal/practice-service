using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class CompanyNameDto
    {
        [Required]
        public string CompanyName { get; set; }
    }
}
