using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodAndStudentPracticeProfileDto
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string PracticePeriodName { get; set; }
        [Required]
        public Guid PracticeProfileId { get; set; }
    }
}
