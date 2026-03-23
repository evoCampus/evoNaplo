namespace evoNaplo.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? GithubUrl { get; set; }
        public string? TrelloUrl { get; set; }
        public string? FigmaUrl { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();
    }
}