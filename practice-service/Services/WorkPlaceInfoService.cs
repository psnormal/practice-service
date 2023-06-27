using practice_service.DTO;
using practice_service.Models;
using System.ComponentModel.DataAnnotations;

namespace practice_service.Services
{
    public class WorkPlaceInfoService : IWorkPlaceInfoService
    {
        private readonly ApplicationDbContext _context;

        public WorkPlaceInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateUpdateWorkPlaceInfo(WorkPlaceInfoCreateUpdateDto model)
        {
            var userInfo = _context.WorkPlaceInfos.FirstOrDefault(p => p.UserId == model.UserId);

            if (userInfo == null)
            {
                var workPlaceInfo = new WorkPlaceInfo
                {
                    CompanyId = model.CompanyId,
                    UserId = model.UserId,
                    Position = model.Position
                };

                await _context.WorkPlaceInfos.AddAsync(workPlaceInfo);
                await _context.SaveChangesAsync();
            }
            else
            {
                userInfo.CompanyId = model.CompanyId;
                userInfo.Position = model.Position;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<WorkPlaceInfoDto> GetWorkPlaceInfo(Guid id)
        {
            var userInfo = _context.WorkPlaceInfos.FirstOrDefault(p => p.UserId == id);

            if (userInfo == null)
            {
                throw new ValidationException("This info does not exist");
            }

            using var client = new HttpClient();
            var url = "https://company-service-6bc8.onrender.com/api/company/name/" + userInfo.CompanyId.ToString();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                CompanyNameDto companyName = await response.Content.ReadFromJsonAsync<CompanyNameDto>();
                WorkPlaceInfoDto result = new WorkPlaceInfoDto
                {
                    CompanyId = userInfo.CompanyId,
                    Position = userInfo.Position,
                    UserId = userInfo.UserId,
                    CompanyName = companyName.CompanyName
                };
                return result;
            }
            else
            {
                throw new ValidationException("This info does not exist");
            }
        }

        public async Task DeleteWorkPlaceInfo(Guid id)
        {
            var userInfo = _context.WorkPlaceInfos.FirstOrDefault(p => p.UserId == id);

            if (userInfo == null)
            {
                throw new ValidationException("This info does not exist");
            }

            _context.WorkPlaceInfos.Remove(userInfo);
            await _context.SaveChangesAsync();
        }
    }
}
