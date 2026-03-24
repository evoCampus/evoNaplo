namespace evoNaplo.Models
{
    public class Team
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset WeeklyMeetingTime { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}