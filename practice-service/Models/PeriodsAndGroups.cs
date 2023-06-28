using System.ComponentModel.DataAnnotations;

namespace practice_service.Models
{
    public class PeriodsAndGroups
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid PracticePeriodId { get; set; }
        [Required]
        public string GroupNumber { get; set; }
    }
}
