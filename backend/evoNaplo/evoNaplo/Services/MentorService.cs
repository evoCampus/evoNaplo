using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal class MentorService : IMentorService
    {
        private static readonly List<Mentor> _mentors = new List<Mentor>();
        private readonly ITeamService _teamService;
        private readonly IProjectService _projectService;
        public MentorService(ITeamService teamService, IProjectService projectService)
        {
            _teamService = teamService;
            _projectService = projectService;
        }

        public IEnumerable<MentorDTO> GetAllMentors()
        {
            return _mentors.Select(mentor => new MentorDTO 
        { 
            Id = mentor.Id, 
            Name = mentor.Name ?? "N/A", 
            Email = mentor.Email ?? "N/A",
            PhoneNumber = mentor.PhoneNumber ?? "N/A",
            TeamIds = mentor.Teams?.Select(team => team.Id).ToList() ?? Enumerable.Empty<string>(),
            ProjectIds = mentor.Projects?.Select(project => project.Id).ToList() ?? Enumerable.Empty<string>(),
        });
        }

        public MentorDTO? GetMentorById(string id)
        {
            Mentor? mentor = _mentors.FirstOrDefault(m => m.Id == id);
            if (mentor is not null) 
                return new MentorDTO 
                {
                    Id = mentor.Id, 
                    Name = mentor.Name ?? "N/A",
                    Email = mentor.Email ?? "N/A",
                    PhoneNumber = mentor.PhoneNumber ?? "N/A",
                    TeamIds = mentor.Teams?.Select(team => team.Id).ToList() ?? Enumerable.Empty<string>(),
                    ProjectIds = mentor.Projects?.Select(project => project.Id).ToList() ?? Enumerable.Empty<string>(),
                };
            else
                return null;
        }

        public void AddMentor(MentorDTO mentor)
        {
            if (string.IsNullOrEmpty(mentor.Id)) 
                mentor.Id = System.Guid.NewGuid().ToString();
            Mentor newMentor = new Mentor
            {
                Id = mentor.Id,
                Name = mentor.Name ?? "N/A",
                Email = mentor.Email ?? "N/A",
                PhoneNumber = mentor.PhoneNumber ?? "N/A",
                Teams = mentor.TeamIds?.Select(teamId => _teamService.GetTeamById(teamId)).OfType<Team>().ToList() ?? new List<Team>(),
                Projects = mentor.ProjectIds?.Select(projectId => _projectService.GetProjectById(projectId)).OfType<Project>().ToList() ?? new List<Project>(),
            };
            _mentors.Add(newMentor);
        }

       
        public void UpdateMentor(string id, MentorDTO updatedMentor)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is null || updatedMentor is null) return;
            if (updatedMentor.Name is not null) existing.Name = updatedMentor.Name ?? "N/A";
            if (updatedMentor.Email is not null) existing.Email = updatedMentor.Email ?? "N/A";
            if (updatedMentor.PhoneNumber is not null) existing.PhoneNumber = updatedMentor.PhoneNumber ?? "N/A";
            if (updatedMentor.TeamIds is not null) existing.Teams = updatedMentor.TeamIds?.Select(teamId => _teamService.GetTeamById(teamId)).OfType<Team>().ToList() ?? new List<Team>();
            if (updatedMentor.ProjectIds is not null) existing.Projects = updatedMentor.ProjectIds?.Select(projectId => _projectService.GetProjectById(projectId)).OfType<Project>().ToList() ?? new List<Project>();
            /*
            if (updatedMentor.TeamCount != 0) existing.TeamCount = updatedMentor.TeamCount;
            if (updatedMentor.ProjectCount != 0) existing.ProjectCount = updatedMentor.ProjectCount;
            if (updatedMentor.StudentCount != 0) existing.StudentCount = updatedMentor.StudentCount;
            */
        }

        public void DeleteMentor(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is not null) _mentors.Remove(existing);
        }
    }
}
