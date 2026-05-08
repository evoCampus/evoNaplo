using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public required IEnumerable<string> MentorIds { get; set; }
    public required IEnumerable<string> StudentIds { get; set; }
    public required DayOfWeek WeeklyMeetingDay { get; set; }
    public required TimeSpan WeeklyMeetingTime { get; set; }
    public required IEnumerable<string> AttendanceSheetIds { get; set; }

    [SetsRequiredMembers]
    public TeamDTO(Team team)
    {
        Id = team.Id;
        MentorIds = team.Mentors.Select(m => m.Id);
        StudentIds = team.Students.Select(s => s.Id);
        //WeeklyMeetingDay = team.WeeklyMeetingDay;
        //WeeklyMeetingTime = team.WeeklyMeetingTime;
        AttendanceSheetIds = team.AttendanceSheets.Select(a => a.Id);
    }
}
