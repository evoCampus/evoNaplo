namespace evoNaplo.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<AttendanceSheet> AttendanceSheets { get; set; }
        public ICollection<Mentor> Mentors { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}