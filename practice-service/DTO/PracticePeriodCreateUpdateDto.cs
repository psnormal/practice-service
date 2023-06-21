using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodCreateUpdateDto
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string PracticeOrder { get; set; }
        [Required]
        public string PracticePeriodName { get; set; }
    }
}
