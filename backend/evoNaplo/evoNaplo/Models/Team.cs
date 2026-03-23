namespace evoNaplo.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? WeeklyMeetingTime { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}