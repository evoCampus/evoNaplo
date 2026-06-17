using evoNaplo.Models;

namespace evoNaplo.DTO.TeamDTOs
{
    public class CreateTeamDTO
    {
        public string? ProjectId { get; set; }
        public string? MentorId { get; set; }
        public string? StudentId { get; set; }
        public string? AttendanceSheetId { get; set; }

        public Team ToTeam()
        {
            return new Team
            {
                Id = string.Empty,
                ProjectId = this.ProjectId,
                Mentors = new List<Mentor>(),
                Students = new List<Student>(),
                AttendanceSheets = new List<AttendanceSheet>()
            };
        }
    }
}
