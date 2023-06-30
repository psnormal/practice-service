using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticeProfileCreateDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Position { get; set; }
        public string Characteristic { get; set; }
        public string PracticeDiary { get; set; }
        [Required]
        public Guid PracticePeriodId { get; set; }
    }
}
