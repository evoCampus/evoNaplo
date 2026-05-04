using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal class TeamService : ITeamService
    {
        private static readonly List<Team> _teams = new List<Team>();
        private readonly IMentorService _mentorService;
        private readonly IStudentService _studentService;

        public TeamService(IMentorService mentorService, IStudentService studentService)
        {
            _mentorService = mentorService;
            _studentService = studentService;
        }

        /// <summary>
        /// Gets the Team model by its ID, including all related entities (mentors, students, attendance sheets).
        /// </summary>
        /// <param name="id">The ID of the team to retrieve.</param>
        /// <returns>The team model if found, otherwise null.</returns>
        public Team? GetTeamModelById(string id)
        {
            return _teams.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Retrieves all teams as DTOs, including their related mentor and student IDs, and attendance sheet IDs.
        /// </summary>
        /// <returns>An enumerable of TeamDTOs representing all teams.</returns>
        public IEnumerable<TeamDTO> GetAllTeams()
        {
            return _teams.Select(team => new TeamDTO
            {
                Id = team.Id,
                MentorIds = team.Mentors?.Select(mentor => mentor.Id) ?? Enumerable.Empty<string>(),
                StudentIds = team.Students?.Select(student => student.Id) ?? Enumerable.Empty<string>(),
                AttendanceSheetIds = team.AttendanceSheets?.Select(sheet => sheet.Id) ?? Enumerable.Empty<string>()
            });
        }

        /// <summary>
        /// Gets a specific team by its ID and returns it as a DTO, including related mentor and student IDs, and attendance sheet IDs.
        /// </summary>
        /// <param name="id">The ID of the team to retrieve.</param>
        /// <returns>The team DTO if found, otherwise null.</returns>
        public TeamDTO? GetTeamById(string id)
        {
            var team = _teams.FirstOrDefault(t => t.Id == id);
            return team is null ? null : new TeamDTO
            {
                Id = team.Id,
                MentorIds = team.Mentors?.Select(mentor => mentor.Id) ?? Enumerable.Empty<string>(),
                StudentIds = team.Students?.Select(student => student.Id) ?? Enumerable.Empty<string>(),
                AttendanceSheetIds = team.AttendanceSheets?.Select(sheet => sheet.Id) ?? Enumerable.Empty<string>()
            };
        }

        /// <summary>
        /// Adds a new team to the database based on the provided TeamDTO. If the DTO does not have an ID, a new one will be generated. The method also resolves mentor and student IDs to their respective models and associates them with the new team.
        /// </summary>
        /// <param name="team">The TeamDTO representing the team to add.</param>
        public void AddTeam(TeamDTO team)
        {
            if (string.IsNullOrEmpty(team.Id)) 
            {
                team.Id = Guid.NewGuid().ToString();
            }
            Team newTeam = new Team
            {
                Id = team.Id,
                Mentors = team.MentorIds?.Select(_mentorService.GetMentorModelById).OfType<Mentor>().ToList() ?? new List<Mentor>(),
                Students = team.StudentIds?.Select(_studentService.GetStudentModelById).OfType<Student>().ToList() ?? new List<Student>(),
            };
            _teams.Add(newTeam);
        }

        /// <summary>
        /// Updates an existing team with the provided values.
        /// </summary>
        /// <param name="id">The ID of the team to update.</param>
        /// <param name="updatedTeam">The updated team DTO.</param>
        public void UpdateTeam(string id, TeamDTO updatedTeam)
        {
            var existing = _teams.FirstOrDefault(t => t.Id == id);
            if (existing is null || updatedTeam is null) 
            {
                return;
            }

            if (updatedTeam.MentorIds is not null) 
            {
                existing.Mentors = updatedTeam.MentorIds?.Select(_mentorService.GetMentorModelById).OfType<Mentor>().ToList() ?? new List<Mentor>();
            }
            if (updatedTeam.StudentIds is not null) 
            {
                existing.Students = updatedTeam.StudentIds?.Select(_studentService.GetStudentModelById).OfType<Student>().ToList() ?? new List<Student>();
            }
        }

        /// <summary>
        /// Deletes a team from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the team to delete.</param>
        public void DeleteTeam(string id)
        {
            var existing = _teams.FirstOrDefault(t => t.Id == id);
            if (existing is not null) 
            {
                _teams.Remove(existing);
            }
        }
    }
}
