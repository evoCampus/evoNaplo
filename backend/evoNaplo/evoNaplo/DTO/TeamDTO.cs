namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public List<string>? Mentors { get; set; }
    public List<string>? Students { get; set; }
    public DayOfWeek WeeklyMeetingDay { get; set; }
    public TimeSpan WeeklyMeetingTime { get; set; }

}
