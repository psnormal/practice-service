using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticeProfileUpdateDto
    {
        [Required]
        public string Position { get; set; }
        [Required]
        public string Characteristic { get; set; }
        [Required]
        public string PracticeDiary { get; set; }
    }
}
