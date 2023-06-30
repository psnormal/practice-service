using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticeProfileUpdateDto
    {
        [Required]
        public string Position { get; set; }
        public string Characteristic { get; set; }
        public string PracticeDiary { get; set; }
    }
}
