namespace practice_service.DTO
{
    public class GroupAndStudentsDto
    {
        public string groupNumber { get; set; }
        public List<UserDto> students { get; set; }
    }
}
