using practice_service.Models;
using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticeProfilePageDto
    {
        [Required]
        public Guid PracticeProfileId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Characteristic { get; set; }
        [Required]
        public string PracticeDiary { get; set; }
        [Required]
        public Guid PracticePeriodId { get; set; }

        public PracticeProfilePageDto(PracticeProfile model)
        {
            PracticeProfileId = model.PracticeProfileId;
            UserId = model.UserId;
            CompanyId = model.CompanyId;
            Position = model.Position;
            Characteristic = model.Characteristic;
            PracticeDiary = model.PracticeDiary;
            PracticePeriodId = model.PracticePeriodId;
        }
    }
}
