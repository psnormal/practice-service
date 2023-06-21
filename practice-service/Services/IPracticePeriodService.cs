using practice_service.DTO;

namespace practice_service.Services
{
    public interface IPracticePeriodService
    {
        Task<Guid> CreatePracticePeriod(PracticePeriodCreateUpdateDto model);
        PracticePeriodPageDto GetPracticePeriodById(Guid id);
        PracticePeriodsDto GetPracticePeriods();
        Task EditPracticePeriod(Guid id, PracticePeriodCreateUpdateDto model);
        Task DeletePracticePeriod(Guid id);
    }
}
