using System.ComponentModel.DataAnnotations;

namespace practice_service.DTO
{
    public class StudentsInPeriodInfoDto
    {
        [Required]
        public List<StudentInPeriodInfoDto> studentInPeriodInfoDtos { get; set; }

        public StudentsInPeriodInfoDto(List<StudentInPeriodInfoDto> studentInPeriodInfoDtos)
        {
            this.studentInPeriodInfoDtos = studentInPeriodInfoDtos;
        }
    }
}
