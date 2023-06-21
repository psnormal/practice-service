using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class StudentPracticeProfiles
    {
        [Required]
        public List<PracticeProfilePageDto> PracticeProfiles { get; set; }

        public StudentPracticeProfiles(List<PracticeProfilePageDto> practiceProfiles)
        {
            PracticeProfiles = practiceProfiles;
        }
    }
}
