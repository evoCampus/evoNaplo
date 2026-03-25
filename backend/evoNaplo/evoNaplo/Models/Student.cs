namespace evoNaplo.Models
{ 
public class Student
    {
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? UniversityProgramme { get; set; }
    public int? CurrentSemester { get; set; }
    public bool IsFirstEvoCampusSemester { get; set; }
    public string? PersonalGoals { get; set; }
    public bool HasAppliedForScholarship { get; set; }
    public bool HasActiveScholarship { get; set; }
    public DateTime ScholarshipDuration { get; set; }
    public bool HasAppliedForInternship { get; set; }
    public bool IsCurrentlyIntern { get; set; }
    public bool IsWorkingStudent { get; set; }
    public DateTime WorkingStudentDuration { get; set; }
    public bool WantsToStayWithCurrentTeam { get; set; }
    public string TeamId { get; set; }
    public Team? Team { get; set; }
    }
}