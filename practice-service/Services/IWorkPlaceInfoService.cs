using practice_service.DTO;
using practice_service.Models;

namespace practice_service.Services
{
    public interface IWorkPlaceInfoService
    {
        Task CreateUpdateWorkPlaceInfo(WorkPlaceInfoCreateUpdateDto model);
        Task<WorkPlaceInfoDto> GetWorkPlaceInfo(Guid id);
        List<WorkPlaceInfo> GetAllWorkPlaceInfosByCompany(int companyId);
        Task DeleteWorkPlaceInfo(Guid id);
    }
}
