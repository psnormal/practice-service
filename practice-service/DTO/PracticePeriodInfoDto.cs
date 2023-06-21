using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodInfoDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string PracticePeriodName { get; set; }
    }
}
