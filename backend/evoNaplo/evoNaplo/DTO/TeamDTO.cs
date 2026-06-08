using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO;

public class TeamDTO
{
    public required string Id { get; set; }
    public required IEnumerable<string> MentorIds { get; set; } = new List<string>();
    public required IEnumerable<string> StudentIds { get; set; } = new List<string>();
    public DayOfWeek WeeklyMeetingDay { get; set; }
    public TimeSpan WeeklyMeetingTime { get; set; }
    public required IEnumerable<string> AttendanceSheetIds { get; set; } = new List<string>();

    [SetsRequiredMembers]
    public TeamDTO()
    {
    }
        [SetsRequiredMembers]
    public TeamDTO(Team team)
    {
        Id = team.Id;
        MentorIds = team.Mentors?.Select(m => m.Id).ToList() ?? new List<string>();
        StudentIds = team.Students?.Select(s => s.Id).ToList() ?? new List<string>();
        AttendanceSheetIds = team.AttendanceSheets?.Select(a => a.Id).ToList() ?? new List<string>();

    }
    
}
