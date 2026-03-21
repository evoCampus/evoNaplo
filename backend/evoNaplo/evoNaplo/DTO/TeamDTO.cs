namespace evoNaplo.DTO;

public class TeamDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }   /// Added because i think its needed, but it can be removed if not
    public List<string>? Mentors { get; set; }
    public List<string>? Students { get; set; }
    public DayOfWeek WeeklyMeetingDay { get; set; }
    public TimeSpan WeeklyMeetingTime { get; set; }

}
