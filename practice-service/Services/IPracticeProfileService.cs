using practice_service.DTO;

namespace practice_service.Services
{
    public interface IPracticeProfileService
    {
        Task<Guid> CreatePracticeProfile(PracticeProfileCreateDto model);
        PracticeProfilePageDto GetPracticeProfileById(Guid id);
        StudentPracticeProfilesDto GetStudentPracticeProfiles(Guid id);
        PracticePeriodsAndStudentPracticeProfilesListDto GetStudentPracticeProfilesAndPeriodsNames(Guid id);
        Task EditPracticeProfiles(Guid id, PracticeProfileUpdateDto model);
        Task DeletePracticeProfile(Guid id);
    }
}
