using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO;

public class StudentDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string UniversityProgramme { get; set; }
    public required int CurrentSemester { get; set; }
    public required bool IsInTheirFirstSemester { get; set; }
    public required string PersonalGoals { get; set; }
    public required bool HasAppliedForScholarship { get; set; }
    public required bool HasScholarship { get; set; }
    public required DateTime ScholarshipDuration { get; set; }
    public required bool HasAppliedForInternship { get; set; }
    public required bool HasInternship { get; set; }
    public required bool IsWorkingStudent { get; set; }
    public required DateTime WorkExperienceInSemesters { get; set; }
    public bool WantsToStayWithCurrentTeam { get; set; }

    [SetsRequiredMembers]
    public StudentDTO(Student student)
    {
        Id = student.Id;
        Name = student.Name;
        Email = student.Email;
        PhoneNumber = student.PhoneNumber;
        UniversityProgramme = student.UniversityProgramme;
        CurrentSemester = student.CurrentSemester;
        IsInTheirFirstSemester = student.IsFirstEvoCampusSemester;
        PersonalGoals = student.PersonalGoals;
        HasAppliedForScholarship = student.HasAppliedForScholarship;
        HasScholarship = student.HasActiveScholarship;
        ScholarshipDuration = student.ScholarshipDuration;
        HasAppliedForInternship = student.HasAppliedForInternship;
        HasInternship = student.IsCurrentlyIntern;
        IsWorkingStudent = student.IsWorkingStudent;
        WorkExperienceInSemesters = student.WorkingStudentDuration;
        WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam;
    }
}
