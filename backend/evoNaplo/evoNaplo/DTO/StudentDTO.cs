namespace evoNaplo.DTO;

public class StudentDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? UniversityProgramme { get; set; }
    public required int CurrentSemester { get; set; }
    public required bool IsInTheirFirstSemester { get; set; }
    public string? PersonalGoals { get; set; }
    public required bool HasAppliedForScholarship { get; set; }
    public required bool HasScholarship { get; set; }
    public required DateTime ScholarshipDuration { get; set; }
    public required bool HasAppliedForInternship { get; set; }
    public required bool HasInternship { get; set; }
    public required bool IsWorkingStudent { get; set; }
    public required int WorkExperienceInSemesters { get; set; }
    public bool WantsToStayWithCurrentTeam { get; set; }

}
