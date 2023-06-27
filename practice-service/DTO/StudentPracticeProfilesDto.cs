using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class StudentPracticeProfilesDto
    {
        [Required]
        public List<PracticeProfilePageDto> PracticeProfiles { get; set; }

        public StudentPracticeProfilesDto(List<PracticeProfilePageDto> practiceProfiles)
        {
            PracticeProfiles = practiceProfiles;
        }
    }
}
