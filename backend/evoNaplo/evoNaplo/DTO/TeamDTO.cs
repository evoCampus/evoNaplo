namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public required IEnumerable<string> MentorIds { get; set; }
    public required IEnumerable<string> StudentIds { get; set; }
    public required DayOfWeek WeeklyMeetingDay { get; set; }
    public required TimeSpan WeeklyMeetingTime { get; set; }
    public required IEnumerable<string> AttendanceSheetIds { get; set; }

}
