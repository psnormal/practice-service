using Newtonsoft.Json;
using practice_service.DTO;
using practice_service.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Text.Json.Nodes;

namespace practice_service.Services
{
    public class PracticePeriodService : IPracticePeriodService
    {
        private readonly ApplicationDbContext _context;

        public PracticePeriodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreatePracticePeriod(string token, PracticePeriodCreateUpdateDto model)
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

            foreach (GroupDto group in model.Groups)
            {
                var info = _context.PeriodsAndGroups.FirstOrDefault(p => p.PracticePeriodId == newPracticePeriod.Id && p.GroupNumber == group.GroupNumber);
                if (info == null)
                {
                    PeriodsAndGroups periodAndGroup = new PeriodsAndGroups
                    {
                        PracticePeriodId = newPracticePeriod.Id,
                        GroupNumber = group.GroupNumber
                    };
                    await _context.PeriodsAndGroups.AddAsync(periodAndGroup);
                    await _context.SaveChangesAsync();

                    await CreateProfilesForGroup(token, group.GroupNumber, newPracticePeriod.Id);
                }
            }

            return newPracticePeriod.Id;
        }

        public PracticePeriodPageDto GetPracticePeriodById(Guid id)
        {
            var practicePeriod = _context.PracticePeriods.FirstOrDefault(p => p.Id == id);

            if (practicePeriod == null)
            {
                throw new ValidationException("This practice period does not exist");
            }

            List<GroupDto> groups = new List<GroupDto>();
            List<PeriodsAndGroups> periodsAndGroups = _context.PeriodsAndGroups.Where(p => p.PracticePeriodId == id).ToList();
            foreach (var group in periodsAndGroups)
            {
                GroupDto groupDto = new GroupDto
                {
                    GroupNumber = group.GroupNumber
                };
                groups.Add(groupDto);
            }

            PracticePeriodPageDto result = new PracticePeriodPageDto
            {
                Id = id,
                StartDate = practicePeriod.StartDate,
                EndDate = practicePeriod.EndDate,
                PracticeOrder = practicePeriod.PracticeOrder,
                PracticePeriodName = practicePeriod.PracticePeriodName,
                Groups = groups
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

        public async Task<StudentsInPeriodInfoDto> GetStudentsInPeriod(string token, Guid id)
        {
            var practicePeriod = _context.PracticePeriods.FirstOrDefault(p => p.Id == id);

            if (practicePeriod == null)
            {
                throw new ValidationException("This practice period does not exist");
            }

            List<PracticeProfile> profilesInPeriod = _context.PracticeProfiles.Where(p => p.PracticePeriodId == id).ToList();
            List<StudentInPeriodInfoDto> studentsProfiles = new List<StudentInPeriodInfoDto>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            foreach (var profile in profilesInPeriod)
            {
                var url2 = "https://hits-user-service.onrender.com/api/users/id/" + profile.UserId;
                var response2 = await client.GetAsync(url2);
                UserDto user = new UserDto();
                if (response2.IsSuccessStatusCode)
                {
                    var jsonString2 = await response2.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDto>(jsonString2);
                }
                else
                {
                    throw new ValidationException("This info does not exist");
                }

                StudentInPeriodInfoDto studentProfile = new StudentInPeriodInfoDto
                {
                    PracticeProfileId = profile.PracticeProfileId,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    patronym = user.patronym,
                    groupNumber = user.groupNumber,
                    userId = user.userId
                };
                studentsProfiles.Add(studentProfile);
            }
            StudentsInPeriodInfoDto result = new StudentsInPeriodInfoDto(studentsProfiles);
            return result;
        }

        public async Task EditPracticePeriod(string token, Guid id, PracticePeriodCreateUpdateDto model)
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

            foreach (GroupDto group in model.Groups)
            {
                var info = _context.PeriodsAndGroups.FirstOrDefault(p => p.PracticePeriodId == id && p.GroupNumber == group.GroupNumber);
                if (info == null)
                {
                    PeriodsAndGroups periodAndGroup = new PeriodsAndGroups
                    {
                        PracticePeriodId = id,
                        GroupNumber = group.GroupNumber
                    };
                    await _context.PeriodsAndGroups.AddAsync(periodAndGroup);
                    await _context.SaveChangesAsync();

                    await CreateProfilesForGroup(token, group.GroupNumber, id);
                }
            }
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

        private async Task CreateProfilesForGroup(string token, string groupNumber, Guid practicePeriod)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var url = "https://hits-user-service.onrender.com/api/groups/" + groupNumber;

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                
                var users = JsonConvert.DeserializeObject<GroupAndStudentsDto>(jsonString);
                //Console.WriteLine(users);
                foreach (var user in users.students)
                {
                    WorkPlaceInfo info = _context.WorkPlaceInfos.FirstOrDefault(p => p.UserId.ToString() == user.userId);

                    PracticeProfileCreateDto profile = null;

                    if (info == null)
                    {
                        profile = new PracticeProfileCreateDto
                        {
                            UserId = new Guid(user.userId),
                            PracticePeriodId = practicePeriod,
                            CompanyId = 1,
                            Position = "",
                            Characteristic = "",
                            PracticeDiary = ""
                        };
                    }
                    else
                    {
                        profile = new PracticeProfileCreateDto
                        {
                            UserId = new Guid(user.userId),
                            PracticePeriodId = practicePeriod,
                            CompanyId = info.CompanyId,
                            Position = info.Position,
                            Characteristic = "",
                            PracticeDiary = ""
                        };
                    }

                    var url2 = "https://practice-service.onrender.com/api/practiceProfile/create";
                    JsonContent content = JsonContent.Create(profile);
                    var response2 = await client.PostAsync(url2, content);
                }
            }
            else
            {
                throw new ValidationException("This info does not exist");
            }
        }
    }
}
