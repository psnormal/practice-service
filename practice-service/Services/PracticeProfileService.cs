using practice_service.DTO;
using practice_service.Models;
using System.ComponentModel.DataAnnotations;

namespace practice_service.Services
{
    public class PracticeProfileService : IPracticeProfileService
    {
        private readonly ApplicationDbContext _context;

        public PracticeProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreatePracticeProfile(PracticeProfileCreateDto model)
        {
            var practicePeriod = _context.PracticePeriods.FirstOrDefault(p => p.Id == model.PracticePeriodId);

            if (practicePeriod == null)
            {
                throw new ValidationException("This practice period does not exist");
            }

            var newPracticeProfile = new PracticeProfile
            {
                UserId = model.UserId,
                CompanyId = model.CompanyId,
                Position = model.Position,
                Characteristic = model.Characteristic,
                PracticeDiary = model.PracticeDiary,
                PracticePeriodId = model.PracticePeriodId
            };

            await _context.PracticeProfiles.AddAsync(newPracticeProfile);
            await _context.SaveChangesAsync();

            return newPracticeProfile.PracticeProfileId;
        }

        public PracticeProfilePageDto GetPracticeProfileById(Guid id)
        {
            var practiceProfile = _context.PracticeProfiles.FirstOrDefault(p => p.PracticeProfileId == id);

            if (practiceProfile == null)
            {
                throw new ValidationException("This practice profile does not exist");
            }

            PracticeProfilePageDto result = new PracticeProfilePageDto(practiceProfile);
            return result;
        }

        public StudentPracticeProfiles GetStudentPracticeProfiles(Guid id)
        {
            List<PracticeProfile> profiles = _context.PracticeProfiles.Where(p => p.UserId == id).ToList();
            List<PracticeProfilePageDto> practiceProfiles = new List<PracticeProfilePageDto>();
            foreach (var profile in profiles)
            {
                PracticeProfilePageDto newProfile = new PracticeProfilePageDto(profile);
                practiceProfiles.Add(newProfile);
            }
            StudentPracticeProfiles result = new StudentPracticeProfiles(practiceProfiles);
            return result;
        }

        public async Task EditPracticeProfiles(Guid id, PracticeProfileUpdateDto model)
        {
            var practiceProfile = _context.PracticeProfiles.FirstOrDefault(p => p.PracticeProfileId == id);

            if (practiceProfile == null)
            {
                throw new ValidationException("This practice profile does not exist");
            }

            practiceProfile.PracticeDiary = model.PracticeDiary;
            practiceProfile.Characteristic = model.Characteristic;
            practiceProfile.Position = model.Position;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePracticeProfile(Guid id)
        {
            var practiceProfile = _context.PracticeProfiles.FirstOrDefault(p => p.PracticeProfileId == id);

            if (practiceProfile == null)
            {
                throw new ValidationException("This practice profile does not exist");
            }
            _context.PracticeProfiles.Remove(practiceProfile);
            await _context.SaveChangesAsync();
        }
    }
}
