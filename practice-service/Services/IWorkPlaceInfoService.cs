using practice_service.DTO;

namespace practice_service.Services
{
    public interface IWorkPlaceInfoService
    {
        Task CreateUpdateWorkPlaceInfo(WorkPlaceInfoCreateUpdateDto model);
        Task<WorkPlaceInfoDto> GetWorkPlaceInfo(Guid id);
        Task DeleteWorkPlaceInfo(Guid id);
    }
}
