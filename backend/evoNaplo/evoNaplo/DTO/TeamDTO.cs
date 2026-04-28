namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public IEnumerable<string>? MentorIds { get; set; }
    public IEnumerable<string>? StudentIds { get; set; }
    public DayOfWeek WeeklyMeetingDay { get; set; }
    public TimeSpan WeeklyMeetingTime { get; set; }
    public IEnumerable<string>? AttendanceSheetIds { get; set; }

}
