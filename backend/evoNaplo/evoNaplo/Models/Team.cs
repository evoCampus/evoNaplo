namespace evoNaplo.Models
{
    public class Team
    {
        public string Id { get; set; }
        public DateTimeOffset WeeklyMeetingTime { get; set; }
        public TimeSpan LengthOfMeeting { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}