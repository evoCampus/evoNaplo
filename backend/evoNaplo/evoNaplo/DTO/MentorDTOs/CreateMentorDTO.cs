using evoNaplo.Models;

namespace evoNaplo.DTO.MentorDTOs
{
    public class CreateMentorDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? TeamId { get; set; }
        public string? ProjectId { get; set; }

        public Mentor toMentor()
        {
            return new Mentor
            {
                Id = string.Empty,
                Name = this.Name,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Teams = new List<Team>(),
                Projects = new List<Project>(),
            };
        }
    }
}
