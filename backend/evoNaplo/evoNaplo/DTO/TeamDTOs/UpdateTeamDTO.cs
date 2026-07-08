namespace evoNaplo.DTO.TeamDTOs
{
    public class UpdateTeamDTO
    {
        public required string Name { get; set; }
        public string? MentorId { get; set; }
        public string? StudentId { get; set; }
        public string? AttendanceSheetId { get; set; }
        public DayOfWeek WeeklyMeetingDay { get; set; }
        public TimeSpan WeeklyMeetingTime { get; set; }
    }
}
