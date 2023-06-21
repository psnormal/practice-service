using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodPageDto
    {
        [Required]
        public Guid Id { get; set; }
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
