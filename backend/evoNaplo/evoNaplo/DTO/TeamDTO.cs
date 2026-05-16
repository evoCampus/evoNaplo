using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public required IEnumerable<string> MentorIds { get; set; }
    public required IEnumerable<string> StudentIds { get; set; }
    public DayOfWeek WeeklyMeetingDay { get; set; }
    public TimeSpan WeeklyMeetingTime { get; set; }
    public required IEnumerable<string> AttendanceSheetIds { get; set; }

    [SetsRequiredMembers]
    public TeamDTO(Team team)
    {
        Id = team.Id;
        MentorIds = team.Mentors.Select(m => m.Id).ToList();
        StudentIds = team.Students.Select(s => s.Id).ToList();
        //WeeklyMeetingDay = team.WeeklyMeetingDay;
        //WeeklyMeetingTime = team.WeeklyMeetingTime;
        AttendanceSheetIds = team.AttendanceSheets.Select(a => a.Id).ToList();

    }
    
}
