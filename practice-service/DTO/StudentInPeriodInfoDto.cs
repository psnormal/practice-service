using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class StudentInPeriodInfoDto
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string groupNumber { get; set; }
        public string patronym { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public Guid PracticeProfileId { get; set; }
    }
}
