namespace evoNaplo.Models
{
    public class AttendanceSheet
    {
        public string Id { get; set; }
        public DateTimeOffset WeeklyMeetingTime { get; set; }
        public TimeSpan LengthOfMeeting { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public ICollection<Student> PresentStudents { get; set; }
        public string TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
