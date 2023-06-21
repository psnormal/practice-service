using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodsDto
    {
        [Required]
        public List<PracticePeriodInfoDto> PracticePeriods { get; set; }

        public PracticePeriodsDto(List<PracticePeriodInfoDto> periods)
        {
            PracticePeriods = periods;
        }
    }
}
