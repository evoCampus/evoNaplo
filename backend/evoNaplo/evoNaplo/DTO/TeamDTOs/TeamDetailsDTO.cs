using evoNaplo.DTO.MentorDTOs;
using evoNaplo.DTO.StudentDTOs;
using evoNaplo.Models;
using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO.TeamDTOs
{
    public class TeamDetailsDTO : TeamDTO
    {
        public IEnumerable<MentorDTO> Mentors { get; set; }
        public IEnumerable<StudentDTO> Students { get; set; }

        [SetsRequiredMembers]
        public TeamDetailsDTO(Team team) : base(team)
        {
            Mentors = team.Mentors?.Select(m => new MentorDTO(m)) ?? Enumerable.Empty<MentorDTO>();
            Students = team.Students?.Select(s => new StudentDTO(s)) ?? Enumerable.Empty<StudentDTO>();
        }
    }
}
