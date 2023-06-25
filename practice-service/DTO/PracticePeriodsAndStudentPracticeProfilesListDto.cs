using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class PracticePeriodsAndStudentPracticeProfilesListDto
    {
        [Required]
        public List<PracticePeriodAndStudentPracticeProfileDto> ProfilesAndPeriodsNames { get; set; }

        public PracticePeriodsAndStudentPracticeProfilesListDto(List<PracticePeriodAndStudentPracticeProfileDto> profilesAndPeriodsNames)
        {
            ProfilesAndPeriodsNames = profilesAndPeriodsNames;
        }
    }
}
