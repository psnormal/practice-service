using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class WorkPlaceInfoCreateUpdateDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
