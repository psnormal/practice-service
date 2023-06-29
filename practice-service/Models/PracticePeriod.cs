using System.ComponentModel.DataAnnotations;

namespace practice_service.Models
{
    public class PracticePeriod
    {
        [Required]
        [Key]
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
