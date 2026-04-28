namespace evoNaplo.DTO;

public class StudentDTO
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? UniversityProgramme { get; set; }
    public int? CurrentSemester { get; set; }
    public bool IsInTheirFirstSemester { get; set; }
    public string? PersonalGoals { get; set; }
    public bool HasAppliedForScholarship { get; set; }
    public bool HasScholarship { get; set; }
    public DateTime ScholarshipDuration { get; set; }
    public bool HasAppliedForInternship { get; set; }
    public bool HasInternship { get; set; }
    public bool IsWorkingStudent { get; set; }
    public int WorkExperienceInSemesters { get; set; }
    public bool WantsToStayWithCurrentTeam { get; set; }

}
