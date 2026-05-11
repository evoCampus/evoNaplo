namespace evoNaplo.Models
{
    public class Team
    {
        public required string Id { get; set; }
        public required string ProjectId { get; set; }
        public Project? Project { get; set; }
        public required ICollection<AttendanceSheet> AttendanceSheets { get; set; }
        public required ICollection<Mentor> Mentors { get; set; }
        public required ICollection<Student> Students { get; set; }
    }
}
