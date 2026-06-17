using evoNaplo.Models;

namespace evoNaplo.DTO.ProjectDTOs
{
    public class CreateProjectDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string> ProjectLinks { get; set; }
        public string? TeamId { get; set; }

        public Project toProject()
        {
            return new Project
            {
                Id = string.Empty,
                Name = this.Name,
                ShortDescription = this.Description,
                ProjectLinks = new List<ProjectLink>(),
                Teams = new List<Team>()
            };
        }
    }
}
