namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public IEnumerable<string>? Mentors { get; set; }
    public IEnumerable<string>? Students { get; set; }
    public DayOfWeek WeeklyMeetingDay { get; set; }
    public TimeSpan WeeklyMeetingTime { get; set; }
    public IEnumerable<IEnumerable<string>>? Attendance {  get; set; }

}
