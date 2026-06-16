using evoNaplo.DTO.TeamDTOs;
using evoNaplo.Models;
using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO.StudentDTOs
{
    public class StudentDetailsDTO : StudentDTO
    {
        public TeamDTO? Team { get; set; }

        [SetsRequiredMembers]
        public StudentDetailsDTO(Student student) : base(student)
        {
            Team = student.Team != null ? new TeamDTO(student.Team) : null;
        }
    }

}
