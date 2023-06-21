using practice_service.DTO;
using practice_service.Models;
using System.ComponentModel.DataAnnotations;

namespace practice_service.Services
{
    public class PracticePeriodService : IPracticePeriodService
    {
        private readonly ApplicationDbContext _context;

        public PracticePeriodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreatePracticePeriod(PracticePeriodCreateUpdateDto model)
        {
            var newPracticePeriod = new PracticePeriod
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                PracticeOrder = model.PracticeOrder,
                PracticePeriodName = model.PracticePeriodName
            };

            await _context.PracticePeriods.AddAsync(newPracticePeriod);
            await _context.SaveChangesAsync();

            return newPracticePeriod.Id;
        }

        public PracticePeriodPageDto GetPracticePeriodById(Guid id)
        {
            var practicePeriod = _context.PracticePeriods.FirstOrDefault(p => p.Id == id);

            if (practicePeriod == null)
            {
                throw new ValidationException("This practice period does not exist");
            }

            PracticePeriodPageDto result = new PracticePeriodPageDto
            {
                Id = id,
                StartDate = practicePeriod.StartDate,
                EndDate = practicePeriod.EndDate,
                PracticeOrder = practicePeriod.PracticeOrder,
                PracticePeriodName = practicePeriod.PracticePeriodName
            };

            return result;
        }

        public PracticePeriodsDto GetPracticePeriods()
        {
            List<PracticePeriod> practicePeriods = _context.PracticePeriods.ToList();
            List<PracticePeriodInfoDto> practicePeriodInfos = new List<PracticePeriodInfoDto>();
            foreach (var period in practicePeriods)
            {
                var newPeriod = new PracticePeriodInfoDto
                {
                    Id = period.Id,
                    PracticePeriodName = period.PracticePeriodName
                };
                practicePeriodInfos.Add(newPeriod);
            }
            PracticePeriodsDto result = new PracticePeriodsDto(practicePeriodInfos);
            return result;
        }

        public async Task EditPracticePeriod(Guid id, PracticePeriodCreateUpdateDto model)
        {
            var practicePeriod = _context.PracticePeriods.FirstOrDefault(p => p.Id == id);

            if (practicePeriod == null)
            {
                throw new ValidationException("This practice period does not exist");
            }

            practicePeriod.StartDate = model.StartDate;
            practicePeriod.EndDate = model.EndDate;
            practicePeriod.PracticePeriodName = model.PracticePeriodName;
            practicePeriod.PracticeOrder = model.PracticeOrder;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePracticePeriod(Guid id)
        {
            var practicePeriod = _context.PracticePeriods.FirstOrDefault(p => p.Id == id);

            if (practicePeriod == null)
            {
                throw new ValidationException("This practice period does not exist");
            }
            _context.PracticePeriods.Remove(practicePeriod);
            await _context.SaveChangesAsync();
        }
    }
}
