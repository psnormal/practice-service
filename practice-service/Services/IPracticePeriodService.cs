using practice_service.DTO;

namespace practice_service.Services
{
    public interface IPracticePeriodService
    {
        Task<Guid> CreatePracticePeriod(string token, PracticePeriodCreateUpdateDto model);
        PracticePeriodPageDto GetPracticePeriodById(Guid id);
        PracticePeriodsDto GetPracticePeriods();
        Task<StudentsInPeriodInfoDto> GetStudentsInPeriod(string token, Guid id);
        Task EditPracticePeriod(string token, Guid id, PracticePeriodCreateUpdateDto model);
        Task DeletePracticePeriod(Guid id);
    }
}
