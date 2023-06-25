using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodAndStudentPracticeProfileDto
    {
        [Required]
        public string PracticePeriodName { get; set; }
        [Required]
        public Guid PracticeProfileId { get; set; }
    }
}
