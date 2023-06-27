using System.ComponentModel.DataAnnotations;

namespace practice_service.Models
{
    public class WorkPlaceInfo
    {
        [Required]
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
